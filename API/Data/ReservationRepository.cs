using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.GetDTOs;
using API.DTOs.PostDTOs;
using API.Interfaces.IRepositories;
using API.Interfaces.IServices;
using API.Models;
using API.Models.Queries;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    class ReservationRepository : IReservationRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IReservationService _reservationService;

        public ReservationRepository(DataContext context, IMapper mapper, IReservationService reservationService)
        {
            _context = context;
            _mapper = mapper;
            _reservationService = reservationService;
        }
        public async Task<IEnumerable<ReservationGetDto>> GetAllReservations()
        {
            return _mapper.Map<List<Reservation>, List<ReservationGetDto>>(await _context.Reservations
                .Include(r => r.Costumer)
                .Include(r => r.ShowTime)
                .ThenInclude(st => st.Movie)
                .Include(r => r.ShowTime)
                .ThenInclude(st => st.Venue)
                .Include(r => r.SeatsPackages)
                .ThenInclude(sp => sp.Seat)
                .ToListAsync());
        }

        public Task<QueryResult<ReservationGetDto>> GetAllReservationsWithQuery(ReservationQuery queryObj)
        {
            throw new NotImplementedException();
        }

        public async Task<ReservationGetDto> GetReservation(Guid id)
        {
            return _mapper.Map<Reservation, ReservationGetDto>(await _context.Reservations
                .Include(r => r.Costumer)
                .Include(r => r.ShowTime)
                .ThenInclude(st => st.Movie)
                .Include(r => r.ShowTime)
                .ThenInclude(st => st.Venue)
                .Include(r => r.SeatsPackages)
                .ThenInclude(sp => sp.Seat)
                .FirstOrDefaultAsync(x => x.Id == id));
        }

        public async Task<ReservationGetDto> Add(ReservationPostDto reservationPostDto)
        {
            Reservation reservation = _mapper.Map<ReservationPostDto, Reservation>(reservationPostDto);
            List<SeatPackage> seatPackages = new List<SeatPackage>();

            foreach (Guid guid in reservationPostDto.SeatPackagesId)
            {
                seatPackages.Add(await _context.SeatPackages
                    .FirstOrDefaultAsync(sp => sp.Id == guid));
            }

            reservation.ShowTime = await _context.ShowTimes.FindAsync(reservationPostDto.ShowTimeId);
            reservation.SeatsPackages = seatPackages;
            reservation.Costumer = await _context.Users.FindAsync(reservationPostDto.CostumerId) as CostumerUser;
            _reservationService.OrderReservation(ref reservation);

            reservation.StartTime = reservation.ShowTime.StartTime;
            _context.Reservations.Add(reservation);

            return _mapper.Map<Reservation, ReservationGetDto>(reservation);
        }

        public async void Remove(Guid id)
        {
            Reservation reservation = await _context.Reservations.FindAsync(id);
            _context.Remove(reservation);
        }

        public void UpdateReservation(Reservation reservation)
        {
            _context.Entry(reservation).State = EntityState.Modified;
        }

        public bool ReservationExists(Guid id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}