using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.GetDTOs;
using API.DTOs.PostDTOs;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    class VenueRepository : IVenueRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public VenueRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<VenueGetDto>> GetAllVenues()
        {
            List<Venue> venues = await _context.Venues
                .Include(v => v.ShowTimes)
                .ThenInclude(st => st.Movie)
                .ToListAsync();
            return _mapper.Map<List<Venue>, List<VenueGetDto>>(venues);
        }

        public async Task<VenueGetDto> GetVenue(int id)
        {
            Venue venue = await  _context.Venues
                .Include(v => v.ShowTimes)
                    .ThenInclude(st => st.Movie)
                .FirstOrDefaultAsync(v => v.VenueNumber == id);
            return _mapper.Map<Venue, VenueGetDto>(venue);
        }

        public VenueGetDto Add(VenuePostDto venuePostDto)
        {
            Venue venue = _mapper.Map<VenuePostDto, Venue>(venuePostDto);
            _context.Venues.AddAsync(venue);
            return _mapper.Map<Venue, VenueGetDto>(venue);
        }

        public async void Remove(int venueId)
        {
            Venue venue = await _context.Venues.FindAsync(venueId);
            _context.Remove(venue);
        }

        public void UpdateVenue(Venue venue)
        {
            _context.Entry(venue).State = EntityState.Modified;
        }

        public bool VenueExists(int id)
        {
            return _context.Venues.Any(e => e.VenueNumber == id);
        }
    }
}