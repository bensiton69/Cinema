using System.Collections.Generic;
using System.Linq;
using API.Data;
using API.Interfaces;
using API.Interfaces.IServices;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    class SeatManagerService : ISeatManagerService
    {
        public void InitSeats(ICollection<Seat> seats)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Seat seat = new Seat()
                    {
                        ColNumber = i,
                        RowNumber = j,
                        IsHandicapped = false
                    };
                    seats.Add(seat);
                }
            }
        }

        public ICollection<SeatPackage> InitSeatPackage(ref Venue venue)
        {
            List<SeatPackage> seatPackages = new List<SeatPackage>();
            foreach (Seat venueSeat in venue.Seats)
            {
                seatPackages.Add(new SeatPackage() { IsAvailable = true, Seat = venueSeat });
            }

            return seatPackages;
        }

        public ICollection<SeatPackage> InitSeatPackage(int venueNumber, DataContext context)
        {
            Venue venue = context.Venues
                .Include(v => v.Seats)
                .FirstOrDefault(v => v.VenueNumber == venueNumber);
            return InitSeatPackage(ref venue);
        }
    }
}