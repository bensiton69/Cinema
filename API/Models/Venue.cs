using System;
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

        public int NumberOfRows { get; set; } = 2;
        public int NumberOfCols { get; set; } = 5;
        public int NumberOfSeats { get; set; }
        public ICollection<ShowTime> ShowTimes { get; set; }
        public ICollection<Seat> Seats { get; set; }

        public Venue()
        {
            ShowTimes = new List<ShowTime>();
            Seats = new List<Seat>();
        }

        private void initSeats()
        {
            NumberOfSeats = NumberOfCols * NumberOfRows;
            for (int i = 0; i < NumberOfCols; i++)
            {
                for (int j = 0; j < NumberOfRows; j++)
                {
                    Seat seat = new Seat()
                    {
                        ColNumber = i,
                        RowNumber = j,
                        IsHandicapped = false
                    };
                    Seats.Add(seat);
                }
            }
        }

        public Venue ShallowCopy()
        {
            return this.MemberwiseClone() as Venue;
        }
    }
}
