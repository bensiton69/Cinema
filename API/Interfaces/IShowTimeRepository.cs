using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs.GetDTOs;
using API.DTOs.PostDTOs;
using API.Models;

namespace API.Interfaces
{
    public interface IShowTimeRepository
    {
        Task<IEnumerable<ShowTimeGetDto>> GetAllShowTimes();
        Task<ShowTimeGetDto> GetShowTime(Guid id);
        ShowTime Add(ShowTimePostDto showTimePostDto);
        void Remove(Guid id);
        void UpdateShowTime(ShowTime showTime);
        bool ShowTimeExists(Guid id);
    }
}
