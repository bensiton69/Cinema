using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs.GetDTOs;
using API.DTOs.PostDTOs;
using API.Models;
using API.Models.Queries;

namespace API.Interfaces.IRepositories
{
    public interface IReservationRepository
    {
        Task<IEnumerable<ReservationGetDto>> GetAllReservations();
        Task<QueryResult<ReservationGetDto>> GetAllReservationsWithQuery(ReservationQuery queryObj);
        Task<ReservationGetDto> GetReservation(Guid id);
        Task<ReservationGetDto> Add(ReservationPostDto reservation);
        void Remove(Guid id);
        void UpdateReservation(Reservation reservation);
        bool ReservationExists(Guid id);
    }
}