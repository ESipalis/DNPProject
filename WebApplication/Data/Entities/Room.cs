using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Data.Entities
{
    public class Room
    {
        public Room() {}

        public Room(string roomId)
        {
            RoomId = roomId;
        }

        [StringLength(10, MinimumLength = 2)]
        [Required]
        [Key]
        public string RoomId {get; set;}

        [MinLength(3)]
        [MaxLength(300)]
        public List<RoomSeat> Seats {get; set;}
    }
}