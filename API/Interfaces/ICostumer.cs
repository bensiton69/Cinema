using System.Collections.Generic;
using API.Models;

namespace API.Interfaces
{
    public interface ICostumer
    {
        public ICollection<Reservation> Reservations { get; set; }
    }
}