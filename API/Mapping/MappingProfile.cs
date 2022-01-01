﻿using API.DTOs;
using API.DTOs.GetDTOs;
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
            CreateMap<ShowTimePostDto, ShowTime>();



            CreateMap<ShowTime, ShowTimeGetDto > ()
                .ForMember(s => s.MovieName, opt => opt.MapFrom(s=> s.Movie.Title))
                .ForMember(s => s.VenueNumber, opt => opt.MapFrom(s => s.Venue.VenueNumber));
            CreateMap<Movie, MoviePostDto>();
            CreateMap<Movie, MovieGetDto>();
            CreateMap<Venue, VenueGetDto>()
                .ForMember(vgd => vgd.ShowTimes, opt => opt.MapFrom(v =>v.ShowTimes));

            CreateMap<ShowTime, KeyValuePairDto>()
                .ForMember(kvp => kvp.Name, opt => opt.MapFrom(st => "Show time of " + st.Movie.Title));
            CreateMap<AppUser, KeyValuePairDto>()
                .ForMember(kvp => kvp.Name, opt => opt.MapFrom(a => a.UserName));

        }
    }

}
