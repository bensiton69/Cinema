using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<KeyValuePairDto>> GetUsers();
        Task<KeyValuePairDto> GetUser(Guid id);
        void Remove(Guid id);
    }
}
