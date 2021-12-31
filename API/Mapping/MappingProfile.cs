using API.DTOs;
using API.DTOs.PostDTOs;
using API.Models;
using AutoMapper;

namespace API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDto, AppUser>();
            CreateMap<MoviePostDto, Movie>();
            CreateMap<VenuePostDto, Venue>();



            CreateMap<Movie, MoviePostDto>();
            CreateMap<AppUser, KeyValuePairDto>()
                .ForMember(kvp => kvp.Name, opt => opt.MapFrom(a => a.UserName));

        }
    }

}
