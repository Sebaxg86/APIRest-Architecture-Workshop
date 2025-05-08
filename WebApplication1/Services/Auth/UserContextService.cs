

using System.Security.Claims;

namespace WebApplication1.Services.Auth;

public class UserContextService: IUserContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;


    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }


    public Guid GetUserId()
    {
        var claim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);


        if (claim == null)
            return new Guid();


        return Guid.Parse(claim.Value);
    }
}
