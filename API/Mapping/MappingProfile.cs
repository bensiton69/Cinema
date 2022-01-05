using System;
using API.DTOs;
using API.DTOs.GetDTOs;
using API.DTOs.PostDTOs;
using API.Models;
using API.Models.Queries;
using AutoMapper;

namespace API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Guid, SeatPackage>()
                .ForMember(sp => sp.Id, opt => opt.MapFrom(g => g));

            CreateMap<RegisterDto, AppUser>();
            CreateMap<MoviePostDto, Movie>();
            CreateMap<VenuePostDto, Venue>();
            CreateMap<ShowTimePostDto, ShowTime>();
            CreateMap<ReservationPostDto, Reservation>()
                .ForMember(r => r.SeatsPackages, opt=> opt.MapFrom(rpt => rpt.SeatPackagesId));

            CreateMap<Reservation, ReservationGetDto>()
                .ForMember(rgd => rgd.ShowTimeKeyValuePairDto, opt => opt.MapFrom(r => r.ShowTime));
            CreateMap<ShowTime, ShowTimeGetDto > ()
                .ForMember(s=> s.SeatPackages, opt => opt.MapFrom(s=>s.SeatPackages))
                .ForMember(s => s.MovieName, opt => opt.MapFrom(s=> s.Movie.Title))
                .ForMember(s => s.VenueNumber, opt => opt.MapFrom(s => s.Venue.VenueNumber));
            CreateMap<Movie, MoviePostDto>();
            CreateMap<Movie, MovieGetDto>();
            CreateMap<Venue, VenueGetDto>()
                .ForMember(vgd => vgd.ShowTimes, opt => opt.MapFrom(v =>v.ShowTimes));

            CreateMap<QueryResult<Movie>, QueryResult<MovieGetDto>>()
                .ForMember(qt => qt.Items, opt => opt.MapFrom(qs => qs.Items));

            CreateMap<ShowTime, KeyValuePairDto>()
                .ForMember(kvp => kvp.Name, opt => opt.MapFrom(st => 
                    "Show time of " + st.Movie.Title + ", in Venue: " +st.VenueId + ", at: " + st.StartTime.TimeOfDay));
            CreateMap<AppUser, KeyValuePairDto>()
                .ForMember(kvp => kvp.Name, opt => opt.MapFrom(a => a.UserName));

        }
    }

}
