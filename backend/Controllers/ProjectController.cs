using Backend.Api.Data;
using Backend.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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

    [HttpGet]
    public async Task<IResult> GetProjects()
    {
        var projects = await _db.Projects.ToListAsync();
        return Results.Ok(projects);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProjectRequest req)
    {
        var project = new Project
        {
            Id = Guid.NewGuid(),
            Name = req.Name,
            OrganizationId = req.OrganizationId,
            CreatedBy = Guid.Parse(
                HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value)
        };

        _db.Projects.Add(project);
        await _db.SaveChangesAsync();

        return Ok(project);
    }
}