using System.Collections.Generic;
using API.Interfaces;

namespace API.Models
{
    public class CostumerUser : AppUser, ICostumer
    {
        public ICollection<Reservation> Reservations { get; set; }

        public CostumerUser()
        {
            Reservations = new List<Reservation>();
        }
    }
}
