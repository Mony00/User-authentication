using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthApiProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        // this rout is protected and is only accessible with a valid JWT
        [Authorize]
        [HttpGet("profile")]
        
        public IActionResult GetUserProfile()
        {
            var username = User.Identity?.Name;

            return Ok(new
            {
                message = "This is a protected profile endpoint.",
                username = username
            });
        }
    }
}