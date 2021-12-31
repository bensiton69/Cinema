using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs.PostDTOs;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class MovieRepository : IMovieRepository
    {
        //TODO work with IMovie interface instead of Movie
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MovieRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<Movie> GetMovie(Guid id)
        {
            return await _context.Movies.FindAsync(id);
        }

        public Movie Add(MoviePostDto moviePostDto)
        {
            Movie movie = _mapper.Map<MoviePostDto, Movie>(moviePostDto);
            _context.Movies.Add(movie);
            return movie;
        }

        public void Remove(Movie movie)
        {
            _context.Remove(movie);
        }

        //TODO: remove id from update

        public void UpdateMovie(Guid id, Movie movie)
        {
            _context.Entry(movie).State = EntityState.Modified;
        }
    }
}
