using WebApplication1.Models;

namespace WebApplication1.Repositories.Users.Users;

public interface IUserRepository: IRepository<User>
{
    public Task<IEnumerable<User>> GetAll();
}
