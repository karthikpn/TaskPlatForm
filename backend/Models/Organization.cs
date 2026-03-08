namespace Backend.Api.Models;

public class Organization
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public Guid OwnerId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}