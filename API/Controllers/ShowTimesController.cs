using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.DTOs.GetDTOs;
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
        public Task<IEnumerable<ShowTimeGetDto>> GetShowTime()
        {
            return _unitOfWork.ShowTimeRepository.GetAllShowTimes();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShowTimeGetDto>> GetShowTime(Guid id)
        {

            if (_unitOfWork.ShowTimeRepository.ShowTimeExists(id) == false)
            {
                return NotFound();
            }

            var showTime = await _unitOfWork.ShowTimeRepository.GetShowTime(id);
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
            _unitOfWork.ShowTimeRepository.Add(showTimePostDto);
            await _unitOfWork.CompleteAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShowTime(Guid id)
        {
            if (_unitOfWork.ShowTimeRepository.ShowTimeExists(id) == false)
            {
                return NotFound();
            }

            _unitOfWork.ShowTimeRepository.Remove(id);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

    }
}
