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
    public class VenuesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public VenuesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public Task<IEnumerable<Venue>> GetVenues()
        {
            return _unitOfWork.VenueRepository.GetAllVenues();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Venue>> GetVenue(Guid id)
        {
            var venue = await _unitOfWork.VenueRepository.GetVenue(id);

            if (venue == null)
            {
                return NotFound();
            }

            return venue;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutVenue(Venue venue)
        {
            _unitOfWork.VenueRepository.UpdateVenue(venue);
            await _unitOfWork.CompleteAsync();
            return Ok(venue);
        }

        [HttpPost]
        public async Task<ActionResult<Venue>> PostVenue(VenuePostDto venuePostDto)
        {
            Venue venue = _unitOfWork.VenueRepository.Add(venuePostDto);
            await _unitOfWork.CompleteAsync();

            return Ok(venue);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenue(Guid id)
        {
            // TODO: use isVenueExists instead
            Venue venue = await _unitOfWork.VenueRepository.GetVenue(id);
            if (venue == null)
            {
                return NotFound();
            }

            _unitOfWork.VenueRepository.Remove(venue);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

    }
}
