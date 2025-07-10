using Microsoft.AspNetCore.Mvc;
using BLL.DTO;
using BLL.Services;

namespace Backend.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly UserService userService = new();
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpPost("add")]
        public IActionResult AddUser([FromBody] UserDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = userService.addUser(user);

            if (response != null)
                return Ok(response);

            return BadRequest("User could not be created.");
        }

        [HttpGet("findAll")]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = userService.findAll(); // or repo directly
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Controller: Failed in GetAllUsers");
                return StatusCode(500, "An error occurred while fetching users.");
            }
        }
    }
}
