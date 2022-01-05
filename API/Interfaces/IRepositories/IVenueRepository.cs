using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs.GetDTOs;
using API.DTOs.PostDTOs;
using API.Models;

namespace API.Interfaces.IRepositories
{
    public interface IVenueRepository
    {
        Task<List<VenueGetDto>> GetAllVenues();
        Task<VenueGetDto> GetVenue(int id);
        VenueGetDto Add(VenuePostDto venuePostDto);
        void Remove(int venueId);
        void UpdateVenue(Venue venue);
        bool VenueExists(int id);
    }
}
