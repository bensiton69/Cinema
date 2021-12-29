using System.Threading.Tasks;
using API.Interfaces;

namespace API.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        public IMovieRepository MovieRepository { get; }

        public UnitOfWork(IMovieRepository movieRepository, DataContext context)
        {
            _context = context;
            MovieRepository = movieRepository;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
