using AutoMapper;
using BLL.DTO;
using BLL.Mapper;
using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService
    {

        public UserDTO addUser(UserDTO userDTO)
        {
            var mapper = UserMapper.CreateMapper();          
            var user = mapper.Map<User>(userDTO);

            var response= DataAccessFactory.userData().addUser(user);
            var data = mapper.Map<UserDTO>(response);
            return data;

        }

        public UserDTO[] findAll()
        {
            var response = DataAccessFactory.userData().findAll();
            var mapper = UserMapper.CreateMapper();
            var data = mapper.Map<UserDTO[]>(response);
            return data;
        }
    }
}
