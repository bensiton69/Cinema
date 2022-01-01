using System.Collections.Generic;

namespace API.DTOs.GetDTOs
{
    public class VenueGetDto
    {
        public int VenueNumber { get; set; }
        public int NumberOfRows { get; set; }
        public int NumberOfCols { get; set; }
        public int NumberOfSeats { get; set; }

        public ICollection<KeyValuePairDto> ShowTimes { get; set; }

        //public ICollection<Seat> AvailableSeats { get; set; }
        //public ICollection<Seat> UnavailableSeats { get; set; }
        //public ICollection<Seat> HandicappedSeats { get; set; }

        public VenueGetDto()
        {
            ShowTimes = new List<KeyValuePairDto>();
            //AvailableSeats = new List<Seat>();
            //UnavailableSeats = new List<Seat>();
            //HandicappedSeats = new List<Seat>();
        }
    }
}
