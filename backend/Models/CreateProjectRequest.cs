namespace Backend.Api.Models;

public class CreateProjectRequest
{
    public string Name { get; set; } = default!;
    public Guid OrganizationId { get; set; }
    public Guid CreatedBy { get; set; }
}