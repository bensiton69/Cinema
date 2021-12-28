using API.DTOs;
using API.Models;
using AutoMapper;

namespace API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDto, AppUser>();
            CreateMap<AppUser, KeyValuePairDto>()
                .ForMember(kvp => kvp.Name, opt => opt.MapFrom(a => a.UserName));

        }
    }

}
