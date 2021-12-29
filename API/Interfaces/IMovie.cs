using System;
using System.Collections.Generic;
using API.Enums;
using API.Models;

namespace API.Interfaces
{
    public interface IMovie
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public string Director { get; set; }
        public eGenre Genre { get; set; }
        public int ProductionYear { get; set; }
        public ICollection<ShowTime> ShowTimes { get; set; }
        //public List<eMovieType> ShowingIn { get; set; }
    }
}
