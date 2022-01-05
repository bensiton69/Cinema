using System.Threading.Tasks;
using API.Interfaces.IRepositories;

namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        IMovieRepository MovieRepository { get; }
        IUserRepository UserRepository { get; }
        IVenueRepository VenueRepository { get; }
        IShowTimeRepository ShowTimeRepository { get; }
        IReservationRepository ReservationRepository { get; }
        Task CompleteAsync();

    }
}
