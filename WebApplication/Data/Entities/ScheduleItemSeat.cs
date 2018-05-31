using System;

namespace WebApplication.Data.Entities
{
    public class ScheduleItemSeat
    {
        public ScheduleItemSeat()
        {
        }

        public ScheduleItemSeat(RoomSeat roomSeat, string occupiedBy)
        {
            RoomSeat = roomSeat;
            OccupiedBy = occupiedBy;
        }

        public ScheduleItem ScheduleItem { get; set; }
        public RoomSeat RoomSeat { get; set; }
        public string OccupiedBy { get; set; }
    }
}