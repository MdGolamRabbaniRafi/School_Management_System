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
        public IActionResult FindAll()
        {
            var response = userService.findAll();

            if (response != null)
                return Ok(response);

            return NotFound("Users not found.");
        }
    }
}
