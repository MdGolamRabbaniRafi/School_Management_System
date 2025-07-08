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
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.ProfilePicture, opt => opt.Ignore()); // this is handled manually during file upload

                cfg.CreateMap<User, UserDTO>()
                    .ForMember(dest => dest.ProfilePicture, opt => opt.Ignore()) // no need to map back a file
                    .ForMember(dest => dest.ProfileImagePath, opt => opt.MapFrom(src => src.ProfilePicture));
                // No automatic mapping from string to IFormFile
            });

            config.CompileMappings(); // Optional, compiles the mappings for performance
            config.AssertConfigurationIsValid(); // Validates the config

            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}
