using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs.GetDTOs;
using API.DTOs.PostDTOs;
using API.Models;
using API.Models.Queries;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IMovieRepository
    {
        Task<IEnumerable<MovieGetDto>> GetAllMovies();
        Task<QueryResult<MovieGetDto>> GetAllMoviesWithQuery(MovieQuery queryObj);
        Task<Movie> GetMovie(Guid id);
        Movie Add(MoviePostDto movie);
        void Remove(Movie movie);
        void UpdateMovie(Movie movie);
    }
}
