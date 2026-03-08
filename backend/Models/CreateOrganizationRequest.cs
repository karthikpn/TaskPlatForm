namespace Backend.Api.Models;

public class CreateOrganizationRequest
{
    public string Name { get; set; } = default!;
    public Guid OwnerId { get; set; }
}