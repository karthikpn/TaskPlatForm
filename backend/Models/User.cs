namespace Backend.Api.Models;

public class User
{
    public Guid Id { get; set; }

    public string OidcSub { get; set; } = default!;

    public string Email { get; set; } = default!;

    public string Name { get; set; } = default!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}