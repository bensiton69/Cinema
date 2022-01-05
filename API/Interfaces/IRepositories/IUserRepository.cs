using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;

namespace API.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<KeyValuePairDto>> GetUsers();
        Task<KeyValuePairDto> GetUser(Guid id);
        void Remove(Guid id);
    }
}
