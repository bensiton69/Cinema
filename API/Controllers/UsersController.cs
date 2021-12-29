using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Authorize]
        public Task<IEnumerable<KeyValuePairDto>> GetUsers()
        {
            return _unitOfWork.UserRepository.GetUsers();
        }

        [Authorize]
        [HttpGet("{id}")]
        public Task<KeyValuePairDto> GetUser(Guid id)
        {
            return _unitOfWork.UserRepository.GetUser(id);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(Guid id)
        {

            var user = await _unitOfWork.UserRepository.GetUser(id);
            if (user == null)
                return NotFound();

            _unitOfWork.UserRepository.Remove(id);
            await _unitOfWork.CompleteAsync();
            return Ok(id);
        }
    }
}