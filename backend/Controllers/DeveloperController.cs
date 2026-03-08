using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers
{
    [ApiController]
    public class DeveloperController : ControllerBase
    {
        ICurrentUser currentUser;
        public DeveloperController(ICurrentUser currentUser)
        {
            this.currentUser = currentUser;
        }

        [HttpGet]
        [Route("/api/me")]
        public IActionResult GetUserDetails()
        {
            return Ok(currentUser);
        }
    }
}
