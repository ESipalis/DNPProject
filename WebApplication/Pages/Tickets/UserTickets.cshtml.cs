using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Data.Entities;

namespace WebApplication.Pages.Tickets
{
    public class UserTicketsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IList<Ticket> Tickets { get; set; }


        public UserTicketsModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            Tickets = await _context.Tickets
                .Include(ticket => ticket.Seat)
                    .ThenInclude(seat => seat.RoomSeat)
                    .ThenInclude(roomSeat => roomSeat.Room)
                .Include(ticket => ticket.Seat)
                    .ThenInclude(seat => seat.ScheduleItem)
                    .ThenInclude(scheduleItem => scheduleItem.Movie)
                .Where(ticket => ticket.Owner.Id == user.Id)
                .ToListAsync();
            return Page();
        }
    }
}