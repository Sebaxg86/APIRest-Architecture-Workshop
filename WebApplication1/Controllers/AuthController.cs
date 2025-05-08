using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs.Users;
using WebApplication1.Services.Auth;

namespace WebApplication1.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;


        public AuthController(AuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserCredentials userCredentials)
        {
            try
            {
                var token = await _authService.Login(userCredentials);
                return Ok(token);
            }
            catch
            {
                return Unauthorized();
            }
        }
        // [HttpPost("internalError")]
        // public  IActionResult Error()
        // {
        //     return StatusCode(500);
        // }
        // [HttpPost("gatewayError")]
        // public  IActionResult Gateway()
        // {
        //     return StatusCode(502);
        // }
    }

