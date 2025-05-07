using System.Text.Json;

namespace WebApplication1.Repositories.Users;

public abstract class FileRepository<T>: IRepository<T> where T : class, IEntity
{
   private readonly string _filePath;
   protected FileRepository(string filePath)
   {
       _filePath = filePath;
   }
   public async Task<T?> Create(T entity)
   {
       try
       {
           entity.Id = Guid.NewGuid();
           var line = JsonSerializer.Serialize(entity);


           var directory = Path.GetDirectoryName(_filePath);
           if (!Directory.Exists(directory))
           {
               Directory.CreateDirectory(directory!);
           }


           await using var writer = new StreamWriter(_filePath, append: true);
           await writer.WriteLineAsync(line);


           return entity;
       }
       catch (Exception e)
       {
           throw new FileRepositoryException();
       }
   }


   public async Task<T?> GetById(Guid? id)
   {
       var records = await GetRecords();
       return records.FirstOrDefault(c => c.Id == id);
   }


   public async Task<IEnumerable<T>> GetAll(Guid userId)
   {
       try
       {
           var records = await GetRecords();
           return records
               .Where(c => c.UserId == userId)
               .ToList();
       }
       catch (Exception e)
       {


           throw new FileRepositoryException();
       }
   }


   public async Task<bool> Delete(Guid id)
   {
       try
       {
           var records = (await GetRecords()).ToList();


           var removedCount = records.RemoveAll(c => c.Id == id);


           if (removedCount == 0)
               return false;


           var lines = records.Select(r => JsonSerializer.Serialize(r));
           await File.WriteAllLinesAsync(_filePath, lines);


           return true;
       }
       catch (Exception e)
       {
           return false;
       }
   }
   protected async Task<IEnumerable<T>> GetRecords()
   {
       try
       {
           if (!File.Exists(_filePath)) return new List<T>();


           var lines = await File.ReadAllLinesAsync(_filePath);


           var records = lines.Select(line => JsonSerializer.Deserialize<T>(line)!);
           return records;
       }
       catch (Exception e)
       {
           throw new FileRepositoryException();
       }
   }
   public async Task<T?> Update(T entity)
   {
       return null;
       try
       {
           var id = entity.Id;
           var records = (await GetRecords()).ToList();


           var index = records.FindIndex(c => c.Id == id);
           if (index == -1)
               return null;
           var original = records[index];
           var incoming = entity;        


           foreach (var prop in typeof(T).GetProperties())
           {


              
               var incomingValue = prop.GetValue(incoming);
               if (incomingValue is not null)
               {
                   prop.SetValue(original, incomingValue);
               }
              
           }
           var lines = records.Select(r => JsonSerializer.Serialize(r));
           await File.WriteAllLinesAsync(_filePath, lines);


           return original;
       }
       catch (Exception e)
       {
           throw new FileRepositoryException();
       }
   }
}
