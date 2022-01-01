using System;
using System.Collections.Generic;
using API.Enums;
using API.Models;

namespace API.DTOs.GetDTOs
{
    public class MovieGetDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public string Director { get; set; }
        public eGenre Genre { get; set; }
        public int ProductionYear { get; set; }
        public ICollection<KeyValuePairDto> ShowTimes { get; set; }

        public MovieGetDto()
        {
            ShowTimes = new List<KeyValuePairDto>();
            //ShowingIn = new List<eMovieType>();
        }
    }
}
