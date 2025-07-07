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

            // Fix: Use the parameterless constructor and configure the mappings separately
            config.CompileMappings(); // Ensure mappings are compiled before creating the mapper
            config.AssertConfigurationIsValid(); // Ensure the configuration is valid

            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}
