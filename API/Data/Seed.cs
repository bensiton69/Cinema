using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using API.DTOs.GetDTOs;
using API.Enums;
using API.Interfaces;
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

        public static async Task SeedVenues(DataContext context, ISeatManagerService seatManagerService)
        {
            if (await context.Venues.AnyAsync()) return;
            
            for (int i = 0; i < NumOfVenuesToSeed; i++)
            {
                Venue venue = new Venue() {VenueNumber = i + 1};
                seatManagerService.InitSeats(venue.Seats);
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

        public static async Task SeedShowTimes(DataContext context, ISeatManagerService seatManagerService)
        {
            if (await context.ShowTimes.AnyAsync()) return;

            Movie movie = await context.Movies.FirstOrDefaultAsync();
            Venue venue = await context.Venues.Include(v => v.Seats).FirstOrDefaultAsync(v => v.VenueNumber == 5);
            DateTime dateTime = DateTime.Now.AddHours(15);

            ShowTime showTime = new ShowTime()
            {
                Movie = movie,
                Venue = venue,
                MovieId = movie.Id,
                StartTime = dateTime,
                SeatPackages = seatManagerService.InitSeatPackage(ref venue),
                VenueId = venue.VenueNumber
            };

            context.Add(showTime);
            await context.SaveChangesAsync();
            
        }

        public static async Task SeedReservation(DataContext context)
        {
            if (await context.Reservations.AnyAsync()) return;

            ShowTime showTime = context.ShowTimes
                .Include(st => st.Venue)
                .Include(st => st.SeatPackages)
                .ThenInclude(sp => sp.Seat)
                .FirstOrDefault();

            List<SeatPackage> seatsPackages = new List<SeatPackage>();
            if (showTime != null)
            {
                seatsPackages.Add(
                    showTime.SeatPackages.FirstOrDefault(sp => sp.Seat.ColNumber == 0 && sp.Seat.RowNumber == 0));
                seatsPackages.Add(
                    showTime.SeatPackages.FirstOrDefault(sp => sp.Seat.ColNumber == 0 && sp.Seat.RowNumber == 1));

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
                context.Reservations.Add(reservation);
            }

            await context.SaveChangesAsync();

        }

    }

}
