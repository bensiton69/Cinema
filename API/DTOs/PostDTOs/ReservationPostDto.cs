using System;
using System.Collections.Generic;

namespace API.DTOs.PostDTOs
{
    public class ReservationPostDto
    {
        public ICollection<Guid> SeatPackagesId { get; set; }
        public Guid ShowTimeId { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime StartTime { get; set; }

        public ReservationPostDto()
        {
            SeatPackagesId = new List<Guid>();
        }
    }
}