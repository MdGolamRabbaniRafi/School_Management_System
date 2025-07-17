using BLL.DTO;
using BLL.Mapper;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Http;

namespace BLL.Services
{
    public class AuthService
    {
        public async Task<UserDTO> AddUserAsync(UserDTO userDTO, HttpRequest httpRequest)
        {
            try
            {
                var emailResponse = EmailService.SendRandomOtpEmail(userDTO.Email);
                if (!emailResponse)
                    throw new Exception("Failed to send OTP email.");

                var mapper = UserMapper.CreateMapper();
                var user = mapper.Map<User>(userDTO);

                user.Password = PasswordHasher.HashPassword(userDTO.Password);

                if (httpRequest.Form.Files.Count > 0)
                {
                    var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads");
                    Directory.CreateDirectory(uploadPath);

                    foreach (var postedFile in httpRequest.Form.Files)
                    {
                        var uniqueFileName = Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                        var filePath = Path.Combine(uploadPath, uniqueFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await postedFile.CopyToAsync(stream);
                        }

                        user.ProfilePicture = Path.Combine("Uploads", uniqueFileName);
                        break;
                    }
                }

                var savedUser = DataAccessFactory.userData().addUser(user);
                if (savedUser == null)
                    throw new Exception("Failed to save user.");

                return mapper.Map<UserDTO>(savedUser);
            }
            catch (Exception ex)
            {
                throw new Exception($"Internal Server Error: {ex.Message}");
            }
        }
    }
}
