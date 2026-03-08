using Backend.Api.Data;
using Backend.Api.Models;
using Microsoft.EntityFrameworkCore;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (await db.Organizations.AnyAsync())
            return;

        var org1 = new Organization
        {
            Id = Guid.NewGuid(),
            Name = "Acme Corp"
        };

        var org2 = new Organization
        {
            Id = Guid.NewGuid(),
            Name = "Globex"
        };

        var projects = new List<Project>
        {
            new Project
            {
                Name = "Acme Project 1",
                OrganizationId = org1.Id
            },
            new Project
            {
                Name = "Acme Project 2",
                OrganizationId = org1.Id
            },
            new Project
            {
                Name = "Globex Project 1",
                OrganizationId = org2.Id
            },
            new Project
            {
                Name = "Globex Project 2",
                OrganizationId = org2.Id
            }
        };

        db.Organizations.AddRange(org1, org2);
        db.Projects.AddRange(projects);

        await db.SaveChangesAsync();
    }
}