using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs.GetDTOs;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestingController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public TestingController(DataContext context, UserManager<AppUser> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpPost("TestShowTime")]
        public async Task<ActionResult> TestShowTime()
        {
            Movie movie = await _context.Movies.FirstOrDefaultAsync();
            Venue venue = await _context.Venues.FirstOrDefaultAsync();
            DateTime dateTime = DateTime.Now.AddHours(9);

            ShowTime showTime = new ShowTime() {Movie = movie, Venue = venue, MovieId = movie.Id, StartTime = dateTime};

            _context.Add(showTime);
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<ShowTime, ShowTimeGetDto>(showTime));
        }


        [HttpPost("TestInterfaces")]
        public async Task<ActionResult> TestInterfaces()
        {
            var users = await _userManager.Users
                .Include(r => r.UserRoles)
                .ThenInclude(r => r.Role)
                .OrderBy(u => u.UserName)
                .ToListAsync();

            List<string> admins = new List<string>();
            List<string> costumers = new List<string>();

            foreach (AppUser appUser in users)
            {
                if(appUser is IAdmin)
                    admins.Add(appUser.UserName);
                if (appUser is ICostumer)
                    costumers.Add(appUser.UserName);
            }
            


            return Ok(new { admins, costumers });

        }

        [HttpGet("TestForMember")]
        [Authorize(Roles = "Member")]
        public async Task<ActionResult> TestForMember()
        {
            return Ok("Success");
        }

        [HttpGet("TestForAdmin")]
        [Authorize(Roles = "AdminUser")]
        public async Task<ActionResult> TestForAdmin()
        {
            return Ok("Success");
        }

        [HttpGet("TestForModerator")]
        [Authorize(Roles = "Moderator")]
        public async Task<ActionResult> TestForModerator()
        {
            return Ok("Success");
        }


    }
}