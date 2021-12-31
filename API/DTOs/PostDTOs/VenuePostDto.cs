using System.ComponentModel.DataAnnotations;

namespace API.DTOs.PostDTOs
{
    public class VenuePostDto
    {
        public int VenueNumber { get; set; }
        public int NumberOfRows { get; set; }
        public int NumberOfCols { get; set; }
    }
}
