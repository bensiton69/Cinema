using System;

namespace API.Models
{
    /// <summary>
    /// This class represents a simple Seat with Availability
    /// </summary>
    public class SeatPackage
    {
        public Guid Id { get; set; }
        public Seat Seat { get; set; }
        public bool IsAvailable { get; set; }
    }
}