using BLL.Common;
using BLL.DTO;
using BLL.Mapper;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Http;

namespace BLL.Services
{
    public class AuthService
    {
        public async Task<string> AddUserAsync(UserDTO userDTO)
        {
            try
            {
                var emailSent = EmailService.SendRandomOtpEmail(userDTO.Email);
                if (!emailSent)
                    throw new Exception("Failed to send OTP email.");

                var environment = Environment.GetEnvironmentVariable("ENVIRONMENT")?.ToLower();
                string rootUploadPath = environment == "production"
                    ? Path.Combine("/mnt/data")
                    : Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

                var uploadPath = Path.Combine(rootUploadPath, "TempUploads");
                Directory.CreateDirectory(uploadPath);

                string? tempImagePath = null;

                if (userDTO.ProfilePicture != null && userDTO.ProfilePicture.Length > 0)
                {
                    var uniqueFileName = Guid.NewGuid() + Path.GetExtension(userDTO.ProfilePicture.FileName);
                    var filePath = Path.Combine(uploadPath, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await userDTO.ProfilePicture.CopyToAsync(stream);
                    }

                    tempImagePath = Path.Combine("TempUploads", uniqueFileName).Replace("\\", "/");
                }

                var redis = RedisHelper.GetDatabase();

                var simplified = new
                {
                    userDTO.Id,
                    userDTO.CreatedAt,
                    userDTO.FirstName,
                    userDTO.Email,
                    userDTO.DateOfBirth,
                    userDTO.Password,
                    userDTO.BloodGroup,
                    userDTO.UserType,
                    ProfileImagePath = tempImagePath
                };

                var json = System.Text.Json.JsonSerializer.Serialize(simplified);
                await redis.StringSetAsync($"user:pending:{userDTO.Email}", json, TimeSpan.FromMinutes(3));

                return "OTP sent successfully. Please verify your email.";
            }
            catch (Exception ex)
            {
                throw new Exception($"Internal Server Error: {ex.Message}", ex);
            }
        }

    }
}
