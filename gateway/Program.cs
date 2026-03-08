using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Yarp.ReverseProxy.Transforms;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie()
.AddOpenIdConnect(options =>
{
    options.Authority = "http://localhost:8080/realms/TaskPlatform";
    options.ClientId = "gateway";
    options.ClientSecret = "tqjWQzQrIaW3QP4w56Gk7VrrD8FjJqsF";
    options.RequireHttpsMetadata = false;

    options.ResponseType = "code";
    options.SaveTokens = true;

    options.Scope.Add("openid");
    options.Scope.Add("profile");
});

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .AddTransforms(context =>
    {
        context.AddRequestTransform(async transformContext =>
        {
            var token = await transformContext.HttpContext.GetTokenAsync("access_token");
            if(!string.IsNullOrEmpty(token))
            {
                transformContext.ProxyRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        });
    });

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapGet("/", () =>
{
    return Results.Redirect("/index.html");
}).RequireAuthorization();

app.MapGet("/logout", async (HttpContext ctx) =>
{
    await ctx.SignOutAsync("Cookies");
    await ctx.SignOutAsync("OpenIdConnect");
});
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapReverseProxy();

app.Run();