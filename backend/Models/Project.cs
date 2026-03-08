namespace Backend.Api.Models;

public class Project
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public Guid OrganizationId { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Organization Organization { get; set; } = default!;
}