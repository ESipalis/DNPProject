using System;

namespace WebApplication.Data.Entities
{
    public class ScheduleItemSeat
    {
        public ScheduleItemSeat()
        {
        }

        public ScheduleItemSeat(ScheduleItem scheduleItem, RoomSeat roomSeat, string occupiedBy)
        {
            ScheduleItem = scheduleItem;
            RoomSeat = roomSeat;
            OccupiedBy = occupiedBy;
        }

        public int Id {get; set;}

        public ScheduleItem ScheduleItem { get; set; }
        public RoomSeat RoomSeat { get; set; }
        public string OccupiedBy { get; set; }
    }
}