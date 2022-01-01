using System;
using API.Interfaces;

namespace API.Models
{
    public class ShowTime
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public Movie Movie { get; set; }
        public Guid MovieId { get; set; }
        public Venue Venue { get; set; }
        public int VenueId { get; set; }

    }
}
