using System;
using API.Interfaces;

namespace API.Models
{
    public class ShowTime
    {
        public Guid Id { get; set; }
        public IMovie Movie { get; set; }
        public DateTime StartTime { get; set; }
        public Venue Venue { get; set; }
    }
}
