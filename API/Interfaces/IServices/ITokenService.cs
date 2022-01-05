using System.Threading.Tasks;
using API.Models;

namespace API.Interfaces.IServices
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}
