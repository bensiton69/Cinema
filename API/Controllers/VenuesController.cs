using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.DTOs.GetDTOs;
using API.DTOs.PostDTOs;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Authorization;

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
        public Task<List<VenueGetDto>> GetVenues()
        {
            return _unitOfWork.VenueRepository.GetAllVenues();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VenueGetDto>> GetVenue(int id)
        {
            var venue = await _unitOfWork.VenueRepository.GetVenue(id);

            if (venue == null)
            {
                return NotFound();
            }

            return venue;
        }

        [Authorize(Roles = "AdminUser")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVenue(Venue venue)
        {
            _unitOfWork.VenueRepository.UpdateVenue(venue);
            await _unitOfWork.CompleteAsync();
            return Ok(venue);
        }

        [HttpPost]
        public async Task<ActionResult<VenueGetDto>> PostVenue(VenuePostDto venuePostDto)
        {
            VenueGetDto venue = _unitOfWork.VenueRepository.Add(venuePostDto);
            await _unitOfWork.CompleteAsync();

            return Ok(venue);
        }

        [Authorize(Roles = "AdminUser")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenue(int id)
        {
            if(_unitOfWork.VenueRepository.VenueExists(id) == false)
            {
                return NotFound();
            }

            _unitOfWork.VenueRepository.Remove(id);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

    }
}
