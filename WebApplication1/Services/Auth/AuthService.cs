using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.DTOs.Users;
using WebApplication1.Models;
using WebApplication1.Repositories.Users.Users;

namespace WebApplication1.Services.Auth;

public class AuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _config;


    public AuthService(IUserRepository userRepository, IConfiguration config)
    {
        _userRepository = userRepository;
        _config = config;
    }


    public async Task<AuthenticationResponse> Login(UserCredentials userCredentials)
    {
        var users = await _userRepository.GetAll();
        var user = users.FirstOrDefault(u => u.Email == userCredentials.Email);
        var passwordValid = user?.Password == userCredentials.Password;
        if (user == null|| !passwordValid) throw new Exception("Invalid credentials");
      
        return GenerateToken(user);
    }


    private AuthenticationResponse GenerateToken(User user)
    {
        var claims = new List<Claim> {
            new Claim("email", user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["jwtKey"] ?? ""));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.UtcNow.AddDays(1);
        var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiration, signingCredentials: creds);
        return new AuthenticationResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
            Expiration = expiration
        };
      
    }
    public Guid GetUserIdFromClaims(ClaimsPrincipal user)
    {
        var claim = user.FindFirst(ClaimTypes.NameIdentifier);
        if (claim == null)
            throw new UnauthorizedAccessException("User ID not found in token.");


        return Guid.Parse(claim.Value);
    }
}