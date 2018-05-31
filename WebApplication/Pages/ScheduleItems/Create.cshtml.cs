using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Data.Entities;

namespace WebApplication.Pages.ScheduleItems
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<SelectListItem> MovieSelections { get; }
        public List<SelectListItem> RoomSelections { get; }
        [BindProperty] public InputModel Input { get; set; }

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
            MovieSelections = _context.Movies
                .Select(movie =>
                    new SelectListItem {Value = movie.Id + "", Text = $"{movie.Title}: {movie.ReleaseDate:dd/MM/yyyy}"})
                .ToList();
            RoomSelections = _context.Rooms
                .Select(room =>
                    new SelectListItem {Value = room.RoomId, Text = $"{room.RoomId}: {room.Seats.Count} seats"})
                .ToList();
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

            Movie movie = await _context.Movies.FindAsync(Input.MovieId);
            Room room = await _context.Rooms
                .Include(roomDb => roomDb.Seats)
                .SingleOrDefaultAsync(roomDb => roomDb.RoomId == Input.RoomId);

            ScheduleItem scheduleItem = new ScheduleItem(movie, room, Input.StartTime, Input.Price);

            _context.ScheduleItems.Add(scheduleItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }


        public class InputModel
        {
            [Required] public int MovieId { get; set; }

            [Required] public string RoomId { get; set; }

            [DisplayName("Start time")]
            [DataType(DataType.DateTime)]
            [Required]
            public DateTime StartTime { get; set; }

            [Range(0, 1000)]
            [DataType(DataType.Currency)]
            [Required]
            public decimal Price { get; set; }
        }
    }
}