using BLL.DTO;
using BLL.Mapper;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Http;

namespace BLL.Services
{
    public class AuthService
    {
        public async Task<UserDTO?> AddUserAsync(UserDTO userDTO, HttpRequest httpRequest)
        {
            var mapper = UserMapper.CreateMapper();
            var user = mapper.Map<User>(userDTO);

            user.Password = PasswordHasher.HashPassword(userDTO.Password);

            if (httpRequest.Form.Files.Count > 0)
            {
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads");

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);

                }
                foreach (var postedFile in httpRequest.Form.Files)
                {
                    var uniqueFileName = Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                    var filePath = Path.Combine(uploadPath, uniqueFileName);

                    // Save file to server
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await postedFile.CopyToAsync(stream);
                        user.ProfilePicture = stream.Name.ToString();

                    }
                    break; 
                }
            }

            var response = DataAccessFactory.userData().addUser(user);
            return response != null ? mapper.Map<UserDTO>(response) : null;
        }
    }
}
