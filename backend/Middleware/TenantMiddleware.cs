using Backend.Api.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

public class TenantMiddleware
{
    private readonly RequestDelegate _next;

    public TenantMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, AppDbContext db)
    {
        var orgIdStr = context.Request.Query["orgId"].FirstOrDefault();

        if (string.IsNullOrEmpty(orgIdStr))
        {
            await _next(context);
            return;
        }

        var sub = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (sub == null)
        {
            context.Response.StatusCode = 401;
            return;
        }

        var user = await db.Users
            .FirstOrDefaultAsync(u => u.OidcSub == sub);

        if (user == null)
        {
            context.Response.StatusCode = 401;
            return;
        }

        var orgId = Guid.Parse(orgIdStr);

        var isMember = await db.Memberships.AnyAsync(m =>
            m.UserId == user.Id &&
            m.OrganizationId == orgId);

        if (!isMember)
        {
            context.Response.StatusCode = 403;
            return;
        }

        await _next(context);
    }
}