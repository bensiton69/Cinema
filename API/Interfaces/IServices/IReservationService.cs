using API.Models;

namespace API.Interfaces.IServices
{
    public interface IReservationService
    {
        public bool OrderReservation(ref Reservation reservation);
    }
}