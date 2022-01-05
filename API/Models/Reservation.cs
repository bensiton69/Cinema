using System;
using System.Collections.Generic;

namespace API.Models
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public Double Price { get; set; }
        public CostumerUser Costumer { get; set; }
        public Guid CostumerId { get; set; }
        public ICollection<SeatPackage> SeatsPackages { get; set; }
        public ShowTime ShowTime { get; set; }
        public Guid ShowTimeId { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime StartTime { get; set; }

        public Reservation()
        {
            SeatsPackages = new List<SeatPackage>();
        }
    }
}