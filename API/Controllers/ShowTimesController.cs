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

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowTimesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShowTimesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public Task<IEnumerable<ShowTime>> GetShowTime()
        {
            return _unitOfWork.ShowTimeRepository.GetAllShowTimes();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShowTime>> GetShowTime(Guid id)
        {
            var showTime = await _unitOfWork.ShowTimeRepository.GetShowTime(id);

            if (showTime == null)
            {
                return NotFound();
            }

            return showTime;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutShowTime(ShowTime showTime)
        {
            _unitOfWork.ShowTimeRepository.UpdateShowTime(showTime);
            await _unitOfWork.CompleteAsync();
            return Ok(showTime);
        }


        [HttpPost]
        public async Task<ActionResult<ShowTime>> PostShowTime(ShowTimePostDto showTimePostDto)
        {
            ShowTime showTime = _unitOfWork.ShowTimeRepository.Add(showTimePostDto);
            await _unitOfWork.CompleteAsync();
            return Ok(showTime);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShowTime(Guid id)
        {
            // TODO: use isVenueExists instead
            ShowTime showTime = await _unitOfWork.ShowTimeRepository.GetShowTime(id);
            if (showTime == null)
            {
                return NotFound();
            }

            _unitOfWork.ShowTimeRepository.Remove(showTime);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

    }
}
