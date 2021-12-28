using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestingController : ControllerBase
    {
        private readonly DataContext _context;

        public TestingController(DataContext context)
        {
            _context = context;
        }


        [HttpPost("TestCostumeUser")]
        public async Task<ActionResult> TestCostumeUser()
        {
            return Accepted();
            //var rolesList = new List<AppUserRole>();
            //rolesList.Add((AppUserRole) "Member");
            //var photographer = new PhotographerUser() { UserName = "testiPhotogrpher", Id = new Guid(), UserRoles = rolesList };
            //await _context.Users.AddAsync(photographer);
            //return Ok(await _context.Users.FindAsync(photographer.Id));
        }

        [HttpGet("TestForMember")]
        [Authorize(Roles = "Member")]
        public async Task<ActionResult> TestForMember()
        {
            return Ok("Success");
        }

        [HttpGet("TestForAdmin")]
        [Authorize(Roles = "Admin")]
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