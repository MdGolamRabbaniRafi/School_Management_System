using AutoMapper;
using BLL.Common;
using BLL.DTO;
using BLL.Mapper;
using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService
    {
        public UserDTO addUser(UserDTO userDTO)
        {
            var mapper = UserMapper.CreateMapper();
            var user = mapper.Map<User>(userDTO);
            user.ProfilePicture = userDTO.ProfileImagePath;
            user.Password = PasswordHasher.HashPassword(userDTO.Password);

            var response = DataAccessFactory.userData().addUser(user);
            return mapper.Map<UserDTO>(response);
        }

        public UserDTO[] findAll()
        {
            var response = DataAccessFactory.userData().findAll();
            var mapper = UserMapper.CreateMapper();
            return mapper.Map<UserDTO[]>(response);
        }

        public async Task<UserDTO?> ConfirmUserRegistrationAsync(string email)
        {
            var redis = RedisHelper.GetDatabase();
            var data = await redis.StringGetAsync($"user:pending:{email}");

            if (!data.HasValue) return null;

            var userJson = data.ToString();
            var userDto = JsonSerializer.Deserialize<UserDTO>(userJson);

            if (userDto == null) return null;

            if (!string.IsNullOrEmpty(userDto.ProfileImagePath))
            {
                var tempPath = Path.Combine("wwwroot", userDto.ProfileImagePath);
                var uploadsFolder = Path.Combine("wwwroot", "Uploads");

                Directory.CreateDirectory(uploadsFolder);

                var fileName = Path.GetFileName(userDto.ProfileImagePath);
                var finalPath = Path.Combine(uploadsFolder, fileName);

                if (System.IO.File.Exists(tempPath))
                {
                    System.IO.File.Move(tempPath, finalPath, overwrite: true);
                    userDto.ProfileImagePath = Path.Combine("Uploads", fileName).Replace("\\", "/");

                }
            }

            // Save user to DB
            var createdUser = this.addUser(userDto);

            // Remove user from redis after successful save
            await redis.KeyDeleteAsync($"user:pending:{email}");

            return createdUser;
        }
    }
}
