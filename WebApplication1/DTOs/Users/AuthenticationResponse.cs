namespace WebApplication1.DTOs.Users;

public class AuthenticationResponse
{
    public required string Token { get; set; }
    
    public DateTime Expiration { get; set; }
}