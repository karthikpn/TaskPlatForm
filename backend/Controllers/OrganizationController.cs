using Backend.Api.Data;
using Backend.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers;

[ApiController]
[Route("api/organizations")]
public class OrganizationController : ControllerBase
{
    private readonly AppDbContext _db;

    public OrganizationController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrganizationRequest request)
    {
        var org = new Organization
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            OwnerId = request.OwnerId
        };

        _db.Organizations.Add(org);
        await _db.SaveChangesAsync();

        return Ok(org);
    }
}