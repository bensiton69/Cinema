using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowTimesController : ControllerBase
    {
        private readonly DataContext _context;

        public ShowTimesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/ShowTimes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShowTime>>> GetShowTime()
        {
            return await _context.ShowTime.ToListAsync();
        }

        // GET: api/ShowTimes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShowTime>> GetShowTime(Guid id)
        {
            var showTime = await _context.ShowTime.FindAsync(id);

            if (showTime == null)
            {
                return NotFound();
            }

            return showTime;
        }

        // PUT: api/ShowTimes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShowTime(Guid id, ShowTime showTime)
        {
            if (id != showTime.Id)
            {
                return BadRequest();
            }

            _context.Entry(showTime).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShowTimeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ShowTimes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShowTime>> PostShowTime(ShowTime showTime)
        {
            _context.ShowTime.Add(showTime);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShowTime", new { id = showTime.Id }, showTime);
        }

        // DELETE: api/ShowTimes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShowTime(Guid id)
        {
            var showTime = await _context.ShowTime.FindAsync(id);
            if (showTime == null)
            {
                return NotFound();
            }

            _context.ShowTime.Remove(showTime);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShowTimeExists(Guid id)
        {
            return _context.ShowTime.Any(e => e.Id == id);
        }
    }
}
