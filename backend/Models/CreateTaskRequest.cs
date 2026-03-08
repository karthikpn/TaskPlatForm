namespace Backend.Api.Models;

public class CreateTaskRequest
{
    public Guid ProjectId { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = "";
}