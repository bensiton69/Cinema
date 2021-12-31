using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs.PostDTOs;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Interfaces
{
    public interface IShowTimeRepository
    {
        Task<IEnumerable<ShowTime>> GetAllShowTimes();
        Task<ShowTime> GetShowTime(Guid id);
        ShowTime Add(ShowTimePostDto showTimePostDto);
        void Remove(ShowTime showTime);
        void UpdateShowTime(ShowTime showTime);
    }

    class ShowTimeRepository : IShowTimeRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ShowTimeRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ShowTime>> GetAllShowTimes()
        {
            return await _context.ShowTime.ToListAsync();
        }

        public async Task<ShowTime> GetShowTime(Guid id)
        {
            return await _context.ShowTime.FindAsync(id);
        }

        public ShowTime Add(ShowTimePostDto showTimePostDto)
        {
            ShowTime showTime = _mapper.Map<ShowTimePostDto, ShowTime>(showTimePostDto);
            _context.Add(showTime);
            return showTime;
        }

        public void Remove(ShowTime showTime)
        {
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
