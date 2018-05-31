using System.ComponentModel.DataAnnotations;

namespace WebApplication.Data.Entities
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required] public ScheduleItem ScheduleItem { get; set; }
        [Required] public ScheduleItemSeat Seat { get; set; }

        [Range(0, 1000)]
        [DataType(DataType.Currency)]
        [Required]
        public decimal PaidPrice { get; set; }

        public string OwnedBy {get; set;}
    }
}