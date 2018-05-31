using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Data.Entities
{
    public class Ticket
    {
        public Ticket()
        {
        }

        public Ticket(ApplicationUser owner, ScheduleItemSeat seat, decimal paidPrice)
        {
            Owner = owner;
            Seat = seat;
            PaidPrice = paidPrice;
        }


        [Required]
        public ScheduleItemSeat Seat { get; set; }

        [Required] public ApplicationUser Owner { get; set; }

        [Range(0, 1000)]
        [DataType(DataType.Currency)]
        [Required]
        public decimal PaidPrice { get; set; }
    }
}