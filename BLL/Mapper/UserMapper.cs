using AutoMapper;
using BLL.DTO;
using DAL.Models;

namespace BLL.Mapper
{
    internal static class UserMapper
    {
        public static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDTO, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); 
                cfg.CreateMap<User, UserDTO>();
            });

            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}
