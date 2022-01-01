﻿using System;
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

        public ShowTimeRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ShowTimeGetDto>> GetAllShowTimes()
        {
            List<ShowTime> showTimes = await _context.ShowTime
                .Include(st => st.Movie)
                .Include(st => st.Venue)
                .ToListAsync();

            return _mapper.Map<List<ShowTime>, List<ShowTimeGetDto>>(showTimes);
        }

        public async Task<ShowTimeGetDto> GetShowTime(Guid id)
        {
            ShowTime showTime = await _context.ShowTime
                .Include(st => st.Movie)
                .Include(st => st.Venue)
                .FirstOrDefaultAsync(st => st.Id == id);

            return _mapper.Map<ShowTime,ShowTimeGetDto>(showTime);

        }

        public ShowTime Add(ShowTimePostDto showTimePostDto)
        {
            ShowTime showTime = _mapper.Map<ShowTimePostDto, ShowTime>(showTimePostDto);
            //Trying to create shallow copy:
            _context.Add(showTime);
            return showTime;
        }

        public async void Remove(Guid id)
        {
            ShowTime showTime = await _context.ShowTime.FindAsync(id);
            _context.Remove(showTime);
        }

        public void UpdateShowTime(ShowTime showTime)
        {
            _context.Entry(showTime).State = EntityState.Modified;
        }

        public bool ShowTimeExists(Guid id)
        {
            return _context.ShowTime.Any(e => e.Id == id);
        }
    }
}