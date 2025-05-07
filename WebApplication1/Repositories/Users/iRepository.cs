namespace WebApplication1.Repositories.Users;

public interface IRepository<T>
{
    public Task<T?> Create(T entity);
    public Task<T?> GetById(Guid? id);
    public Task<T?> Update(T entity);
    public Task<IEnumerable<T>> GetAll(Guid userId);
    public Task<bool> Delete(Guid id);
}
