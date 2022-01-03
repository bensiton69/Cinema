using System.Collections.Generic;
using API.Data;
using API.Models;

namespace API.Interfaces
{
    public interface ISeatManagerService
    {
        void InitSeats(ICollection<Seat> Seats);
        ICollection<SeatPackage> InitSeatPackage(ref Venue venue);
        ICollection<SeatPackage> InitSeatPackage(int venueNumber, DataContext context);
    }
}