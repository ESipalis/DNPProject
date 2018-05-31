using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace WebApplication.Data.Entities
{
    public class ScheduleItem
    {
        public int Id { get; set; }

        public ScheduleItem() {}

        public ScheduleItem(Movie movie, Room room, DateTime startTime, decimal price)
        {
            Movie = movie;
            Room = room;
            StartTime = startTime;
            Price = price;
            Seats = Room.Seats.Select(seat => new ScheduleItemSeat(this, seat, null)).ToList();
            Tickets = new List<Ticket>();
        }

        [Required] public Movie Movie { get; set; }

        [DisplayName("Start time")]
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime StartTime { get; set; }

        [DisplayName("End time")]
        [NotMapped]
        public DateTime EndTime => StartTime.Add(Movie.Duration);

        [DisplayName("Duration")]
        [NotMapped]
        public TimeSpan Duration => Movie.Duration;

        [Required] public Room Room { get; set; }
        public List<Ticket> Tickets { get; set; }

        public List<ScheduleItemSeat> Seats {get; set;}

        [Range(0, 1000)]
        [DataType(DataType.Currency)]
        [Required]
        public decimal Price { get; set; }
    }
}