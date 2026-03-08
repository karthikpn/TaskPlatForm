namespace Backend.Api.Models;

public class TaskItem
{
    public Guid Id { get; set; }

    public Guid ProjectId { get; set; }

    public string Title { get; set; } = default!;

    public string Description { get; set; } = "";

    public string Status { get; set; } = "Todo";

    public Guid? AssignedTo { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Project Project { get; set; } = default!;
}