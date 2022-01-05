using System;
using System.Collections.Generic;

namespace API.DTOs.PostDTOs
{
    public class ReservationPostDto
    {
        public ICollection<Guid> SeatPackagesId { get; set; }
        public Guid ShowTimeId { get; set; }
        public Guid CostumerId { get; set; }

        public ReservationPostDto()
        {
            SeatPackagesId = new List<Guid>();
        }
    }
}