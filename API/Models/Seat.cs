using System;

namespace API.Models
{
    public class Seat
    {
        public Guid Id { get; set; }
        public int RowNumber { get; set; }
        public int ColNumber { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsHandicapped { get; set; }

    }
}
