using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Venue
    {
        public Guid Id { get; set; }
        public int VenueNumber { get; set; }
        public int NumberOfRows { get; set; }
        public int NumberOfCols { get; set; }
        public ICollection<Seat> AvailableSeats { get; set; }
        public ICollection<Seat> UnavailableSeats { get; set; }
        public ICollection<Seat> HandicappedSeats { get; set; }

        public Venue()
        {
            AvailableSeats = new List<Seat>();
            UnavailableSeats = new List<Seat>();
            HandicappedSeats = new List<Seat>();
        }
    }
}
