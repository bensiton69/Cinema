using System.Collections.Generic;
using API.Models;

namespace API.DTOs.GetDTOs
{
    public class VenueGetDto
    {
        public int VenueNumber { get; set; }
        public int NumberOfRows { get; set; }
        public int NumberOfCols { get; set; }
        public int NumberOfSeats { get; set; }

        public ICollection<KeyValuePairDto> ShowTimes { get; set; }
        public ICollection<Seat> Seats { get; set; }

        public VenueGetDto()
        {
            ShowTimes = new List<KeyValuePairDto>();
            Seats = new List<Seat>();
        }
    }
}
