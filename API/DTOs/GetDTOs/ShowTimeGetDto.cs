using System;
using API.Models;

namespace API.DTOs.GetDTOs
{
    public class ShowTimeGetDto
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public Guid MovieId { get; set; }
        public string MovieName { get; set; }
        public int VenueNumber { get; set; }
    }
}
