using System;
using System.Collections.Generic;
using API.Models;

namespace API.DTOs.GetDTOs
{
    public class ReservationGetDto
    {
        public Guid Id { get; set; }
        public Double Price { get; set; }
        public ICollection<SeatPackage> SeatsPackages { get; set; }
        public ShowTimeGetDto ShowTimeGetDto { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime StartTime { get; set; }

        public ReservationGetDto()
        {
            SeatsPackages = new List<SeatPackage>();
        }
    }
}