using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IEnumerable<Venue>> GetAllVenues()
        {
            return await _context.Venues.ToListAsync();
        }

        public async Task<Venue> GetVenue(Guid id)
        {
            return await _context.Venues.FindAsync(id);
        }

        public Venue Add(VenuePostDto venuePostDto)
        {
            Venue venue = _mapper.Map<VenuePostDto, Venue>(venuePostDto);
            _context.Venues.AddAsync(venue);
            return venue;
        }

        public void Remove(Venue venue)
        {
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