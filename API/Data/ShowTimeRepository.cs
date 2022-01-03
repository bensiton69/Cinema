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
    class ShowTimeRepository : IShowTimeRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ISeatManagerService _seatManagerService;

        public ShowTimeRepository(DataContext context, IMapper mapper, ISeatManagerService seatManagerService)
        {
            _context = context;
            _mapper = mapper;
            _seatManagerService = seatManagerService;
        }
        public async Task<IEnumerable<ShowTimeGetDto>> GetAllShowTimes()
        {
            List<ShowTime> showTimes = await _context.ShowTimes
                .Include(st=> st.SeatPackages)
                .ThenInclude(sp => sp.Seat)
                .Include(st => st.Movie)
                .Include(st => st.Venue)
                .ToListAsync();

            return _mapper.Map<List<ShowTime>, List<ShowTimeGetDto>>(showTimes);
        }

        public async Task<ShowTimeGetDto> GetShowTime(Guid id)
        {
            ShowTime showTime = await _context.ShowTimes
                .Include(st => st.Movie)
                .Include(st => st.Venue)
                .FirstOrDefaultAsync(st => st.Id == id);

            return _mapper.Map<ShowTime,ShowTimeGetDto>(showTime);

        }

        public async Task<ShowTime> Add(ShowTimePostDto showTimePostDto)
        {
            ShowTime showTime = _mapper.Map<ShowTimePostDto, ShowTime>(showTimePostDto);
            showTime.SeatPackages = _seatManagerService.InitSeatPackage(showTimePostDto.VenueId, _context);
            await _context.AddAsync(showTime);
            return showTime;
        }


        public async void Remove(Guid id)
        {
            ShowTime showTime = await _context.ShowTimes.FindAsync(id);
            _context.Remove(showTime);
        }

        public void UpdateShowTime(ShowTime showTime)
        {
            _context.Entry(showTime).State = EntityState.Modified;
        }

        public bool ShowTimeExists(Guid id)
        {
            return _context.ShowTimes.Any(e => e.Id == id);
        }
    }
}