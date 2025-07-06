using BLL.DTO;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Backend.Controllers
{
    [RoutePrefix("user")]
    public class UserController : ApiController
    {
        UserService userService = new UserService();
        [HttpPost]
        [Route("add")]
        public IHttpActionResult addUser(UserDTO user)
        {
            if (!ModelState.IsValid)
            {
                // Return 400 Bad Request with validation errors
                return BadRequest(ModelState);
            }
            var response = userService.addUser(user);

            if (response != null)
                return Ok(response); // 200 OK with user object

            return BadRequest("User could not be created."); // 400 Bad Request
        }
    }
}