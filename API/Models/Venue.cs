using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Venue
    {
        [Key]
        public int VenueNumber { get; set; }
        public int RowsNumber { get; set; }
        public int ColsNumber { get; set; }
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
