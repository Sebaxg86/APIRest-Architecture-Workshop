using WebApplication1.DTOs.Users;
using WebApplication1.Models;

namespace WebApplication1.Mappers;


    public class UserMapper
    {
        public static User  InsertDtoToUser(UserInsertDto? userInstertDto) {


            return new User
            {
                Email = userInstertDto?.Email,
                Name = userInstertDto?.Name,
                Password = userInstertDto?.Password,
            };
        }


        public static UserReadDto UserToReadDto(User? user)
        {
            return new UserReadDto
            {
                Name = user?.Name,
                Email = user?.Email
            };
        }
    }


