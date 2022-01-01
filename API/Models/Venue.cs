﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Venue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VenueNumber { get; set; }
        public int NumberOfRows { get; set; }
        public int NumberOfCols { get; set; }
        public int NumberOfSeats { get; set; }

        public ICollection<ShowTime> ShowTimes { get; set; }

        //public ICollection<Seat> AvailableSeats { get; set; }
        //public ICollection<Seat> UnavailableSeats { get; set; }
        //public ICollection<Seat> HandicappedSeats { get; set; }

        public Venue()
        {
            ShowTimes = new List<ShowTime>();
            //AvailableSeats = new List<Seat>();
            //UnavailableSeats = new List<Seat>();
            //HandicappedSeats = new List<Seat>();
        }
    }
}
