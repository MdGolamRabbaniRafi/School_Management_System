using BLL.DTO;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService authService = new();

        [HttpPost("signUp")]
        public async Task<IActionResult> SignUp([FromForm] UserDTO userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var httpRequest = HttpContext.Request;


            var result = await authService.AddUserAsync(userDto, httpRequest);

            if (result != null)
                return Ok(result);

            return BadRequest("User could not be created.");
        }
    }
}
