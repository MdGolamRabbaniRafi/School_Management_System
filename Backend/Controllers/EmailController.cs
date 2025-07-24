using BLL.DTO;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("email")]
    public class EmailController : ControllerBase
    {
        private readonly EmailService emailService = new();
        private readonly ILogger<EmailController> _logger;

        public EmailController(ILogger<EmailController> logger)
        {
            _logger = logger;
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp(string email, string otp)
        {
            var user = await emailService.VerifyOtpAsync(email, otp);

            if (user != null)
                return Ok(user);

            return BadRequest("Invalid or expired OTP.");
        }


    }
}
