using Microsoft.AspNetCore.Mvc;
using BLL.Services;

namespace Backend.Controllers
{
    [ApiController]
    [Route("health")]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet("check")]
        public IActionResult Index()
        {
            string message = HealthCheckServices.getHealthHealthCheck();
            return Ok(new { message });
        }
    }
}
