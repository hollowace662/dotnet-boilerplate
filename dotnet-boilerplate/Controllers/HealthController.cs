using Microsoft.AspNetCore.Mvc;

namespace dotnet_boilerplate.Controllers
{
    [ApiController]
    [Route("/")]
    public class HealthController : ControllerBase
    {
        public IActionResult GetHealth()
        {
            return Ok(new { status = "Healthy" });
        }
    }
}
