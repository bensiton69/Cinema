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

        [HttpGet("GetReservations")]
        public async Task<IActionResult> GetReservations()
        {
            return Ok(_mapper.Map<List<Reservation>, List<ReservationGetDto>>(await _context.Reservations
                .Include(r => r.ShowTime)
                .Include(r=> r.SeatsPackages)
                .ToListAsync()));
        }

        [HttpGet("GetNumberOfSeatPackages")]
        public async Task<IActionResult> GetNumberOfSeatPackages()
        {
            return Ok(_context.SeatPackages.Count());
        }

        [HttpPost("PostReservations")]
        public async Task<IActionResult> PostReservations()
        {
            ShowTime showTime = _context.ShowTimes
                .Include(st => st.Venue)
                .Include(st => st.SeatPackages)
                .ThenInclude(sp => sp.Seat)
                .FirstOrDefault();

            List<SeatPackage> seatsPackages = new List<SeatPackage>();
            seatsPackages.Add(showTime.SeatPackages.FirstOrDefault(sp=>sp.Seat.ColNumber==0&& sp.Seat.RowNumber == 0));
            seatsPackages.Add(showTime.SeatPackages.FirstOrDefault(sp=>sp.Seat.ColNumber==0&& sp.Seat.RowNumber == 1));

            foreach (SeatPackage seatsPackage in seatsPackages)
            {
                seatsPackage.IsAvailable = false;
            }

            Reservation reservation = new Reservation()
            {
                OrderTime = DateTime.Now,
                ShowTimeId = showTime.Id,
                SeatsPackages = seatsPackages,
                Price = 3.75 * seatsPackages.Count,
                StartTime = DateTime.Now.AddHours(12)
            };
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("GetSeats")]
        public async Task<IActionResult> GetSeats()
        {
            return Ok(await _context.Seats.ToListAsync());
        }

        [HttpGet("TestShallowness")]
        public async Task<IActionResult> TestShallowness()
        {
            Dictionary<string, Seat> seatsDictionary = new Dictionary<string, Seat>();
            

            ShowTime showTime = await _context.ShowTimes
                .Include(st => st.SeatPackages)
                .ThenInclude(sp => sp.Seat)
                .FirstOrDefaultAsync();

            Venue venue = await _context.Venues
                .Include(v => v.Seats)
                .FirstOrDefaultAsync();


            Seat seatShowTime = showTime.SeatPackages.FirstOrDefault().Seat;
            Seat seatVenue = venue.Seats.FirstOrDefault(s => s.Id == seatShowTime.Id);
            seatsDictionary.Add("Before: Seat from ShowTimes:", seatShowTime.ShallowCopy());
            seatsDictionary.Add("Before: Seat from venue:", seatVenue.ShallowCopy());

            venue.Seats.FirstOrDefault(s => s.RowNumber == 0 && s.ColNumber == 0).IsHandicapped = true;

            seatsDictionary.Add("After: Seat from ShowTimes:", seatShowTime);
            seatsDictionary.Add("After: Seat from venue:", seatVenue);

            return Ok(seatsDictionary);
        }


        [HttpPost("TestShowTime")]
        public async Task<ActionResult> TestShowTime()
        {
            Movie movie = await _context.Movies.FirstOrDefaultAsync();
            Venue venue = await _context.Venues.Include(v=> v.Seats).FirstOrDefaultAsync(v=>v.VenueNumber==5);
            DateTime dateTime = DateTime.Now.AddHours(15);

            ShowTime showTime = new ShowTime()
            {
                Movie = movie,
                Venue = venue, 
                MovieId = movie.Id, 
                StartTime = dateTime, 
                SeatPackages = initSeatPackage(ref venue),
                VenueId = venue.VenueNumber
            };

            _context.Add(showTime);
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<ShowTime, ShowTimeGetDto>(showTime));
        }

        [HttpGet("TestVenueCopyFromShowTimes")]
        public async Task<IActionResult> TestVenueCopyFromShowTimes()
        {
            Dictionary<string, Seat> seatsDictionary = new Dictionary<string, Seat>();
            List<ShowTime> showTimes = await _context.ShowTimes
                .Include(st => st.SeatPackages)
                .ThenInclude(sp => sp.Seat)
                .Include(st => st.Movie)
                .Include(st => st.Venue)
                .ToListAsync();

            Venue venue = await _context.Venues
                .Include(v => v.Seats)
                .FirstOrDefaultAsync(x => x.VenueNumber == showTimes[0].VenueId);

            
            seatsDictionary.Add("Seat from venue:", venue.Seats.FirstOrDefault());
            seatsDictionary.Add("Seat from showtime.SeatPackage:", showTimes
                    .FirstOrDefault()
                    .SeatPackages.FirstOrDefault()
                    .Seat);


            return Ok(seatsDictionary);
        }

        [HttpGet("TestVenueCopy")]
        public async Task<IActionResult> TestVenueCopy()
        {
            Dictionary<string, VenueGetDto> venuesDictionary = new Dictionary<string, VenueGetDto>();

            Venue v1 = await _context.Venues
                .Include(v => v.Seats)
                .Include(v => v.ShowTimes)
                .ThenInclude(st => st.Movie)
                .FirstOrDefaultAsync();

            Venue v2 = v1.ShallowCopy();
            Venue v3 = _context.Venues
                .AsNoTracking()
                .Include(v => v.Seats)
                .Include(v => v.ShowTimes)
                .ThenInclude(st => st.Movie)
                .Single(v => v.VenueNumber == v1.VenueNumber);


            //init all seats of v1 and v2 to isHandicapped = true:
            foreach (Seat seat in v2.Seats)
            {
                //seat.IsAvailable = true;
                seat.IsHandicapped = true;
            }
            foreach (Seat seat in v1.Seats)
            {
                //seat.IsAvailable = false;
                seat.IsHandicapped = false;
            }

            foreach (Seat seat in v3.Seats)
            {
                //seat.IsAvailable = true;
                seat.IsHandicapped = false;
            }

            venuesDictionary.Add("Before: v1:", _mapper.Map<Venue, VenueGetDto>(v1));
            venuesDictionary.Add("Before: v2:", _mapper.Map<Venue, VenueGetDto>(v2));
            venuesDictionary.Add("Before: v3:", _mapper.Map<Venue, VenueGetDto>(v3));

            //foreach (Seat seat in v2.Seats)
            //{
            //    seat.IsHandicapped = true;
            //}

            //venuesDictionary.Add("After: v1:", _mapper.Map<Venue, VenueGetDto>(v1));
            //venuesDictionary.Add("After: v2:", _mapper.Map<Venue, VenueGetDto>(v2));

            return Ok(venuesDictionary);

        }

        [HttpGet("TestVenueSeats")]
        public async Task<IActionResult> TestVenueSeats()
        {
            Dictionary<string, Seat> dictionarySeats = new Dictionary<string, Seat>();

            Venue v1 = await _context.Venues
                .Include(v =>v.Seats)
                .Include(v => v.ShowTimes)
                .ThenInclude(st => st.Movie)
                .FirstOrDefaultAsync();

            Seat seatFromV1 = v1.Seats.FirstOrDefault();

            ShowTime showTime = await _context.ShowTimes
                .Include(st => st.Venue)
                .ThenInclude(v=> v.Seats)
                .FirstOrDefaultAsync();

            Seat seat = showTime.Venue.Seats.FirstOrDefault();

            if (seat == null)
                return BadRequest();

            Venue v2 = v1;
            Seat seatFromV2 = seat.ShallowCopy();
            // Initialize all seats to true:

            //seatFromV1.IsAvailable = true;
            //seatFromV2.IsAvailable = true;
            //seat.IsAvailable = true;

            dictionarySeats.Add("Before: seat from show time: ", seat.ShallowCopy());
            dictionarySeats.Add("Before: seat from venue", seatFromV1.ShallowCopy());
            dictionarySeats.Add("Before: seat from venue 2", seatFromV2.ShallowCopy());

            //seatFromV2.IsAvailable = false;
            
            dictionarySeats.Add("After: seat from show time: ", seat);
            dictionarySeats.Add("After: seat from venue", seatFromV1);
            dictionarySeats.Add("After: seat from venue 2", seatFromV2);
            return Ok(dictionarySeats);

        }


        private ICollection<SeatPackage> initSeatPackage(ref Venue venue)
        {
            List<SeatPackage> seatPackages = new List<SeatPackage>();
            foreach (Seat venueSeat in venue.Seats)
            {
                seatPackages.Add(new SeatPackage(){IsAvailable = true, Seat = venueSeat});
            }

            return seatPackages;
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