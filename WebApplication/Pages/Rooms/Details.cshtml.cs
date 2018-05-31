using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Data.Entities;

namespace WebApplication.Pages.Rooms
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public Room Room { get; set; }

        public DetailsModel(ApplicationDbContext context)
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
    }
}
