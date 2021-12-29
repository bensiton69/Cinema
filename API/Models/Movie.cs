using System;
using System.Collections.Generic;
using API.Enums;
using API.Interfaces;

namespace API.Models
{
    public class Movie :IMovie
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public string Director { get; set; }
        public eGenre Genre { get; set; }
        public int ProductionYear { get; set; }
        public ICollection<ShowTime> ShowTimes { get; set; }
        //public List<eMovieType> ShowingIn { get; set; }

        public Movie()
        {
            ShowTimes = new List<ShowTime>();
            //ShowingIn = new List<eMovieType>();
        }
    }
}
