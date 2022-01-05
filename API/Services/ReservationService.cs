using System;
using API.Interfaces.IServices;
using API.Models;

namespace API.Services
{
    class ReservationService : IReservationService
    {
        private readonly object _reservationLock = new object();
        public bool OrderReservation(ref Reservation reservation)
        {
            foreach (SeatPackage seatPackage in reservation.SeatsPackages)
            {
                lock (_reservationLock)
                {
                    if (seatPackage.IsAvailable == false)
                    {
                        throw new Exception("Seats are taken!");
                    }

                    lock (_reservationLock)
                    {
                        seatPackage.IsAvailable = false;
                        reservation.Price = 2.6 * reservation.SeatsPackages.Count;
                    }
                }
            }

            return true;
        }
    }
}