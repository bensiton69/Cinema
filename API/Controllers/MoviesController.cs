using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.DTOs.PostDTOs;
using API.Interfaces;
using API.Models;
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
        public async Task<IEnumerable<Movie>> GetMovies()
        {
            return await _unitOfWork.MovieRepository.GetAllMovies();
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
        public async Task<IActionResult> PutMovie(Guid id, Movie movie)
        {
            _unitOfWork.MovieRepository.UpdateMovie(id, movie);
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
