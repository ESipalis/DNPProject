using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication.Data;
using WebApplication.Data.Entities;

namespace WebApplication.Pages.Rooms
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        [BindProperty] public InputModel Input { get; set; }

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Room room = new Room(Input.RoomId);
            room.Seats = Input.SeatsString.Split(",").Select(seatString => new RoomSeat(room, seatString.Trim())).ToList();

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }


        public class InputModel
        {
            [Required]
            public string RoomId {get; set;}

            [Required]
            public string SeatsString {get; set;}
        }
    }
}