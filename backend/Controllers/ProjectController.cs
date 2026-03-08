using Backend.Api.Data;
using Backend.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers;

[ApiController]
[Route("api/projects")]
public class ProjectController : ControllerBase
{
    private readonly AppDbContext _db;

    public ProjectController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProjectRequest req)
    {
        var project = new Project
        {
            Id = Guid.NewGuid(),
            Name = req.Name,
            OrganizationId = req.OrganizationId,
            CreatedBy = req.CreatedBy
        };

        _db.Projects.Add(project);
        await _db.SaveChangesAsync();

        return Ok(project);
    }
}