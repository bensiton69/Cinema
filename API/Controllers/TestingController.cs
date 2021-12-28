using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Interfaces;
using API.Models;
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

        public TestingController(DataContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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