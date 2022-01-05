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
using API.Interfaces.IRepositories;
using API.Interfaces.IServices;
using API.Models;
using AutoMapper;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReservationsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IEnumerable<ReservationGetDto>> GetReservation()
        {
            return await _unitOfWork.ReservationRepository.GetAllReservations();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationGetDto>> GetReservation(Guid id)
        {
            if (_unitOfWork.ReservationRepository.ReservationExists(id) == false)
            {
                return NotFound();
            }

            var reservation = await _unitOfWork.ReservationRepository.GetReservation(id);
            return reservation;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(Reservation reservation)
        {
            _unitOfWork.ReservationRepository.UpdateReservation(reservation);
            await _unitOfWork.CompleteAsync();
            return Ok();
        }


        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(ReservationPostDto reservationPostDto)
        {
            ReservationGetDto reservationGetDto = await _unitOfWork.ReservationRepository.Add(reservationPostDto);
            await _unitOfWork.CompleteAsync();
            return Ok(reservationGetDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(Guid id)
        {
            if (_unitOfWork.ReservationRepository.ReservationExists(id) == false)
            {
                return NotFound();
            }

            _unitOfWork.ReservationRepository.Remove(id);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
