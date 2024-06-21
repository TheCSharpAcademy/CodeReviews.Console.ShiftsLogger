using Microsoft.AspNetCore.Mvc;

namespace Shiftlogger.javedkhan2k2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetHealth()
        {
            return Ok(new { status = "Healthy" });
        }
    }
}