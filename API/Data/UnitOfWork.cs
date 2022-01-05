using System.Threading.Tasks;
using API.Interfaces;
using API.Interfaces.IRepositories;

namespace API.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        public IMovieRepository MovieRepository { get; }
        public IUserRepository UserRepository { get; }
        public IVenueRepository VenueRepository { get; }
        public IShowTimeRepository ShowTimeRepository { get; }
        public IReservationRepository ReservationRepository { get; }

        public UnitOfWork(
            IMovieRepository movieRepository,
            DataContext context,
            IUserRepository userRepository,
            IVenueRepository venueRepository,
            IShowTimeRepository showTimeRepository,
            IReservationRepository reservationRepository)
        {
            _context = context;
            MovieRepository = movieRepository;
            UserRepository = userRepository;
            VenueRepository = venueRepository;
            ShowTimeRepository = showTimeRepository;
            ReservationRepository = reservationRepository;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
