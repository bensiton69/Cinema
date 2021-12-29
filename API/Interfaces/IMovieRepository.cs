using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs.PostDTOs;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAllMovies();
        Task<Movie> GetMovie(Guid id);
        Movie Add(MoviePostDto movie);
        void Remove(Movie movie);
        void UpdateMovie(Guid id, Movie movie);
    }
}
