using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using WebApplication.Data;
using WebApplication.Data.Entities;

namespace WebApplication.Pages.Tickets
{
    public class BuyTicketModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ScheduleItem ScheduleItemView { get; set; }
        [BindProperty] public InputModel Input { get; set; }
        public List<SelectListItem> SeatSelections { get; private set; }

        public BuyTicketModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ScheduleItemView = await _context.ScheduleItems
                .Include(item => item.Seats).ThenInclude(seat => seat.RoomSeat)
                .Include(item => item.Movie)
                .Include(item => item.Room)
                .FirstOrDefaultAsync(item => item.Id == id);
            if (ScheduleItemView == null)
            {
                return NotFound();
            }

            SeatSelections = ScheduleItemView.Seats
                .Where(seat => seat.OccupiedBy == null)
                .Select(seat => new SelectListItem {Value = seat.Id + "", Text = seat.RoomSeat.SeatId})
                .ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            if (id == null)
            {
                return NotFound();
            }

            ScheduleItem scheduleItem = await _context.ScheduleItems
                .Include(item => item.Seats).ThenInclude(seat => seat.RoomSeat)
                .Include(item => item.Movie)
                .Include(item => item.Room)
                .FirstOrDefaultAsync(item => item.Id == id);
            if (scheduleItem == null)
            {
                return NotFound();
            }

            ScheduleItemSeat chosenSeat = scheduleItem.Seats.Find(m => m.Id == Input.SeatId);
            if (chosenSeat == null)
            {
                return NotFound();
            }
            if(chosenSeat.OccupiedBy != null)
            {
                return NotFound();
            }

            chosenSeat.OccupiedBy = Input.Name;
            decimal finalPrice = scheduleItem.Price;
            if (Input.DiscountCode == "Manager")
            {
                finalPrice *= 0.8m;
            }

            var user = await _userManager.GetUserAsync(User);
            var ticket = new Ticket(user, chosenSeat, finalPrice);

            _context.Tickets.Add(ticket);
            _context.SaveChanges(true);


            return RedirectToPage("./UserTickets");
        }


        public class InputModel
        {
            [Required] public int SeatId { get; set; }
            [Required] public string Name { get; set; }
            public string DiscountCode { get; set; }
        }
    }
}