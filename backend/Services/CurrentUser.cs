using System.Security.Claims;

public class CurrentUser : ICurrentUser
{
    public string? UserId { get; }
    public string? Email { get; }

    public CurrentUser(IHttpContextAccessor accessor)
    {
        var user = accessor.HttpContext?.User;

        UserId = user?.FindFirst("sub")?.Value;
        Email = user?.FindFirst(ClaimTypes.Email)?.Value;
    }
}