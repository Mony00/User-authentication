using Microsoft.AspNetCore.Mvc;

namespace AuthApiProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok(new { message = "API is working!" });
        }
    }
}
