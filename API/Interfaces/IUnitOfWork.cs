using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        IMovieRepository MovieRepository { get; }
        IUserRepository UserRepository { get; }
        IVenueRepository VenueRepository { get; }
        IShowTimeRepository ShowTimeRepository { get; }
        Task CompleteAsync();

    }
}
