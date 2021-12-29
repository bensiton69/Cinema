﻿using System.Threading.Tasks;
using API.Interfaces;

namespace API.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        public IMovieRepository MovieRepository { get; }
        public IUserRepository UserRepository { get; }

        public UnitOfWork(IMovieRepository movieRepository, DataContext context, IUserRepository userRepository)
        {
            _context = context;
            MovieRepository = movieRepository;
            UserRepository = userRepository;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}