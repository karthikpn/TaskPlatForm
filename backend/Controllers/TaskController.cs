using Backend.Api.Data;
using Backend.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Api.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskController : ControllerBase
    {
        readonly AppDbContext _db;

        public TaskController(AppDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskRequest req)
        {
            var task = new TaskItem
            {
                Id = Guid.NewGuid(),
                ProjectId = req.ProjectId,
                Title = req.Title,
                Description = req.Description
            };

            _db.Tasks.Add(task);
            await _db.SaveChangesAsync();

            return Ok(task);
        }

        [HttpGet("{projectId}")]
        public async Task<IActionResult> GetTasks(Guid projectId)
        {
            var tasks = await _db.Tasks
                .Where(t => t.ProjectId == projectId)
                .ToListAsync();

            return Ok(tasks);
        }
    }
}
