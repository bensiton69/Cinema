using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs.PostDTOs;
using API.Models;

namespace API.Interfaces
{
    public interface IVenueRepository
    {
        Task<IEnumerable<Venue>> GetAllVenues();
        Task<Venue> GetVenue(Guid id);
        Venue Add(VenuePostDto venuePostDto);
        void Remove(Venue venue);
        void UpdateVenue(Venue venue);
    }
}
