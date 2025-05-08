using System.Text.Json;
using WebApplication1.Models;

namespace WebApplication1.Repositories.Users.Users;

public class FileUserRepository : IUserRepository
{
    private readonly string _filePath = "Data/users.txt";
    
    public async Task<User?> GetById(Guid? id)
    {
        var records = await GetRecords();
        return records.FirstOrDefault(r => r.Id == id);
    }


    public Task<User?> Update(User entity)
    {
        throw new NotImplementedException();
    }


    public  Task<IEnumerable<User>> GetAll(Guid userId)
    {
        throw new NotImplementedException();
    }


    public Task<bool> Delete(Guid id)
    {
        throw new NotImplementedException();
    }


    public async Task<IEnumerable<User>> GetAll()
    {
        var records = await GetRecords();
        return records;
    }


    private async Task<IEnumerable<User>> GetRecords()
    {
        if (!File.Exists(_filePath)) return new List<User>();


        var lines = await File.ReadAllLinesAsync(_filePath);


        var records = lines.Select(line => JsonSerializer.Deserialize<User>(line)!);
        return records;
    }


    async Task<User?> IRepository<User>.Create(User entity)
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
            Console.WriteLine(e);
            throw;
        }
    }



}