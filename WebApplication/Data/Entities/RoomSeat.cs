using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApplication.Data.Entities
{
    public class RoomSeat
    {
        public RoomSeat() {}

        public RoomSeat(Room room, string seatId)
        {
            Room = room;
            SeatId = seatId;
        }

        [Required]
        public string SeatId {get; set;}

        [Required]
        [ForeignKey("RoomId")]
        public Room Room {get; set;}

        public override string ToString()
        {
            return SeatId;
        }
    }
}