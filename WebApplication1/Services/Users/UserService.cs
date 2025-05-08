using WebApplication1.DTOs.Users;
using WebApplication1.Mappers;
using WebApplication1.Repositories.Users.Users;
using WebApplication1.Services.Auth;

namespace WebApplication1.Services.Users;

public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly Guid? _userId;
    public UserService(IUserRepository userRepository, IUserContextService userContextService)
    {
        _userRepository = userRepository;
        _userId = userContextService.GetUserId();
    }


    public async Task<UserReadDto?> RegisterUser(UserInsertDto userInsertDto)
    {
        var user = UserMapper.InsertDtoToUser(userInsertDto);
        return  UserMapper.UserToReadDto(await _userRepository.Create(user));
    }
    public async Task<UserReadDto?> GetUser()
    {
        var user = await _userRepository.GetById(_userId);


        return UserMapper.UserToReadDto(user);
    }


    // public async Task<IEnumerable<UserReadDto>> GetAllUsers()
    // {
    //     var users = await _userRepository.GetAll();
    //    
    //     return users.Select(UserMapper.UserToReadDto).ToList();
    // }

}