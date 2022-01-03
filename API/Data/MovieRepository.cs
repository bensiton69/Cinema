using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using API.DTOs;
using API.DTOs.GetDTOs;
using API.DTOs.PostDTOs;
using API.Extensions;
using API.Interfaces;
using API.Models;
using API.Models.Queries;
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
        public async Task<IEnumerable<MovieGetDto>> GetAllMovies()
        {
            var movies = await _context.Movies
                .Include(m => m.ShowTimes)
                .ToListAsync();
            return _mapper.Map<List<Movie>, List<MovieGetDto>>(movies);
        }

        public async Task<QueryResult<MovieGetDto>> GetAllMoviesWithQuery(MovieQuery queryObj)
        {
            var result = new QueryResult<Movie>();
            var query = _context.Movies
                .Include(m=> m.ShowTimes)
                .AsQueryable();

            //Filtering
            if (String.IsNullOrEmpty(queryObj.Title) == false)
                query = query.Where(m => m.Title == queryObj.Title);
            if (queryObj.Genre.HasValue)
                query = query.Where(m => (int)m.Genre == queryObj.Genre.Value);

            var columnsMap = new Dictionary<string, Expression<Func<Movie, object>>>()
            {
                ["genre"] = m => m.Genre,
                ["title"] = st => st.Title,
            };

            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();


            return _mapper.Map<QueryResult<Movie>, QueryResult<MovieGetDto>>(result);
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

        public void UpdateMovie(Movie movie)
        {
            _context.Entry(movie).State = EntityState.Modified;
        }
    }
}
