using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using API.DTOs.GetDTOs;
using API.Enums;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static int NumOfVenuesToSeed { get; set; } = 5;

        public static async Task SeedUsers(UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<CostumerUser>>(userData);
            if (users == null) return;

            var roles = new List<AppRole>
            {
                new AppRole{Name = "Member"},
                new AppRole{Name = "AdminUser"},
                new AppRole{Name = "Moderator"},
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            foreach (var user in users)
            {
                user.UserName = user.UserName.ToLower();
                await userManager.CreateAsync(user, "Pa$$w0rd");
                await userManager.AddToRoleAsync(user, "Member");
            }

            var admin = new AdminUser()
            {
                UserName = "admin"
            };

            await userManager.CreateAsync(admin, "Pa$$w0rd");
            await userManager.AddToRolesAsync(admin, new[] { "AdminUser", "Moderator" });
        }

        public static async Task SeedVenues(DataContext context)
        {
            if (await context.Venues.AnyAsync()) return;
            ICollection<Seat> seats = new List<Seat>();
            
            for (int i = 0; i < NumOfVenuesToSeed; i++)
            {
                Venue venue = new Venue() {VenueNumber = i + 1};
                initSeats(venue.Seats);
                await context.AddAsync(venue);
                await context.SaveChangesAsync();
            }
        }


        public static async Task SeedMovies(DataContext context)
        {
            if (await context.Movies.AnyAsync()) return;

            List<Movie> movies = new List<Movie>();

            movies.Add(new Movie()
            {
                Director = "benny",
                Duration = 126,
                Genre = eGenre.Action,
                ProductionYear = 1997,
                Title = "Benny and the thieves"
            });
            movies.Add(new Movie()
            {
                Director = "David",
                Duration = 153,
                Genre = eGenre.Mystery,
                ProductionYear = 2005,
                Title = "Harry Potter and the order of the phoenix"
            });

            foreach (Movie movie in movies)
            {
                await context.AddAsync(movie);
                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedShowTimes(DataContext _context)
        {
            if (await _context.ShowTimes.AnyAsync()) return;

            Movie movie = await _context.Movies.FirstOrDefaultAsync();
            Venue venue = await _context.Venues.Include(v => v.Seats).FirstOrDefaultAsync(v => v.VenueNumber == 5);
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
            
        }

        private static void initSeats(ICollection<Seat> Seats)
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Seat seat = new Seat()
                    {
                        ColNumber = i,
                        RowNumber = j,
                        IsHandicapped = false
                    };
                    Seats.Add(seat);
                }
            }
        }
        private static ICollection<SeatPackage> initSeatPackage(ref Venue venue)
        {
            List<SeatPackage> seatPackages = new List<SeatPackage>();
            foreach (Seat venueSeat in venue.Seats)
            {
                seatPackages.Add(new SeatPackage() { IsAvailable = true, Seat = venueSeat });
            }

            return seatPackages;
        }

    }

}
