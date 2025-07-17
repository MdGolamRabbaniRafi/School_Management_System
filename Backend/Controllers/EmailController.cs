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

        [HttpPost("verifyOTP")]
        public IActionResult VerifyOTP([FromBody] OtpVerificationDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Otp))
            {
                return BadRequest("Email and OTP are required.");
            }

            bool isVerified = emailService.VerifyOtp(request.Email, request.Otp);

            if (isVerified)
                return Ok(new { message = "OTP verified successfully." });

            return BadRequest(new { message = "Invalid or expired OTP." });
        }

    }
}
