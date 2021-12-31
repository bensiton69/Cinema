using System;
using API.Models;

namespace API.DTOs.PostDTOs
{
    public class ShowTimePostDto
    {
        public DateTime StartTime { get; set; }
        public int VenueId { get; set; }
        public Guid MovieId { get; set; }
    }
}
