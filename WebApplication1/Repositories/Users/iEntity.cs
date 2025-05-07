namespace WebApplication1.Repositories.Users;

public interface IEntity
{
    Guid? Id { get; set; }
    Guid? UserId { get; set; }
}
