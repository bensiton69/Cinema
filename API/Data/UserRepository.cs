using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<KeyValuePairDto>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<IEnumerable<AppUser>, List<KeyValuePairDto>>(users);
        }

        public async Task<KeyValuePairDto> GetUser(Guid id)
        {
            return _mapper.Map<AppUser, KeyValuePairDto>(await _context.Users.FindAsync(id));
        }

        public async void Remove(Guid id)
        {
            _context.Remove(await _context.Users.FindAsync(id));
        }
    }
}