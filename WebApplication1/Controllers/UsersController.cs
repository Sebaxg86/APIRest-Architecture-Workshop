using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs.Users;
using WebApplication1.Services.Users;

namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;


    public UsersController(UserService userService)
    {
        _userService = userService;
    }


    [HttpPost]
    public async Task<IActionResult> Register([FromBody] UserInsertDto userInsertDto)
    {
        var user = await _userService.RegisterUser(userInsertDto);
        if (user is null) return Conflict();
        return Ok(user);
    }
    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    // public Task<ActionResult<UserReadDto>> Get()
    public async Task<IActionResult> Get()
    {
        var users = await _userService.GetUser();
        return Ok(users);
    }
  


}

