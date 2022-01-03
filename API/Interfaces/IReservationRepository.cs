using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs.GetDTOs;
using API.DTOs.PostDTOs;
using API.Models;
using API.Models.Queries;

namespace API.Interfaces
{
    public interface IReservationRepository
    {
        Task<IEnumerable<ReservationGetDto>> GetAllReservations();
        Task<QueryResult<ReservationGetDto>> GetAllReservationsWithQuery(ReservationQuery queryObj);
        Task<Reservation> GetReservation(Guid id);
        Reservation Add(ReservationPostDto reservation);
        void Remove(Reservation reservation);
        void UpdateReservation(Guid id, Reservation reservation);
    }

}