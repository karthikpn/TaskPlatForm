namespace Backend.Api.Models;

public class Membership
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid OrganizationId { get; set; }

    public string Role { get; set; } = "Member";

    public User User { get; set; } = default!;

    public Organization Organization { get; set; } = default!;
}