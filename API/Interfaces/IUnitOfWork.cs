using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        IMovieRepository MovieRepository { get; }
        Task CompleteAsync();

    }
}
