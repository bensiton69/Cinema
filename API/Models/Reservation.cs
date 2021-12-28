using System;
using System.Collections.Generic;

namespace API.Models
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public Double Price { get; set; }
        public ICollection<Seat> Seats { get; set; }
        public ShowTime ShowTime { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime StartTime { get; set; }

        public Reservation()
        {
            Seats = new List<Seat>();
        }
    }
}