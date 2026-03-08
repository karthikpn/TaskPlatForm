using Backend.Api.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;

namespace Backend.Api.Middleware
{
    public class UserProvisioningMiddleware
    {
        readonly RequestDelegate _next;

        public UserProvisioningMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, AppDbContext appDbContext)
        {
            if(context.User.Identity?.IsAuthenticated == true)
            {
                var sub = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var email = context.User.FindFirst(ClaimTypes.Email)?.Value;
                var name = context.User.FindFirst("name")?.Value;

                if(sub != null)
                {
                    var user = await appDbContext.Users.FirstOrDefaultAsync(u => u.OidcSub.Equals(sub));

                    if(user == null)
                    {
                        if(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email))
                        {
                            throw new InvalidOperationException("User details not found!");
                        }
                        user = new Models.User
                        {
                            Id = Guid.NewGuid(),
                            Name = name,
                            Email = email,
                            OidcSub = sub,
                        };

                        appDbContext.Users.Add(user);
                        await appDbContext.SaveChangesAsync();
                    }
                }
            }
            await _next(context);
        }
    }
}
