using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Parsing.Structure.IntermediateModel;
using WebApplication.Data;
using WebApplication.Data.Entities;

namespace WebApplication.Pages.Rooms
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public Room Room { get; set; }
        [BindProperty] public InputModel Input { get; set; }

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Room = await _context.Rooms.Include(room => room.Seats).SingleOrDefaultAsync(m => m.RoomId == id);

            if (Room == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Room room = await _context.Rooms
                .Include(m => m.Seats)
                .SingleOrDefaultAsync(m => m.RoomId == id);


            var newSeats = Input.SeatsString.Split(",")
                .Select(seatString => new RoomSeat(room, seatString.Trim()))
                .ToList();

            room.Seats.RemoveAll(oldSeat => !newSeats.Any(newSeat => newSeat.SeatId == oldSeat.SeatId)); // Remove the removed ones
            newSeats.RemoveAll(newSeat => room.Seats.Any(oldSeat => oldSeat.SeatId == newSeat.SeatId)); // Skip already existing ones
            room.Seats.AddRange(newSeats); // Add new ones

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(Room.RoomId))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToPage("./Index");
        }

        private bool RoomExists(string id)
        {
            return _context.Rooms.Any(e => e.RoomId == id);
        }

        public class InputModel
        {
            [Required] public string SeatsString { get; set; }
        }
    }
}