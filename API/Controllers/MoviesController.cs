using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.DTOs.GetDTOs;
using API.DTOs.PostDTOs;
using API.Interfaces;
using API.Models;
using API.Models.Queries;
using AutoMapper;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public MoviesController( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IEnumerable<MovieGetDto>> GetMovies()
        {
            return await _unitOfWork.MovieRepository.GetAllMovies();
        }

        [HttpGet("WithQuery")]
        public async Task<QueryResult<MovieGetDto>> GetMoviesWithQuery([FromQuery] MovieQuery movieQuery)
        {
            return await _unitOfWork.MovieRepository.GetAllMoviesWithQuery(movieQuery);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(Guid id)
        {
            var movie = await _unitOfWork.MovieRepository.GetMovie(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(Movie movie)
        {
            _unitOfWork.MovieRepository.UpdateMovie(movie);
            await _unitOfWork.CompleteAsync();
            return Ok(movie);
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(MoviePostDto moviePostDto)
        {
            Movie movie = _unitOfWork.MovieRepository.Add(moviePostDto);
             await _unitOfWork.CompleteAsync();

            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(Guid id)
        {
            Movie movie = await _unitOfWork.MovieRepository.GetMovie(id);
            if (movie == null)
            {
                return NotFound();
            }

            _unitOfWork.MovieRepository.Remove(movie);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

    }
}
