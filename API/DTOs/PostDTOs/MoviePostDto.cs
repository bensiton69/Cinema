using System;
using System.Collections.Generic;
using API.Enums;
using API.Models;

namespace API.DTOs.PostDTOs
{
    public class MoviePostDto
    {
        public string Title { get; set; }
        public int Duration { get; set; }
        public string Director { get; set; }
        public eGenre Genre { get; set; }
        public int ProductionYear { get; set; }

    }
}
