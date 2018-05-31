using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Data.Entities;

namespace WebApplication.Pages.ScheduleItems
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public ScheduleItem ScheduleItem { get; set; }

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ScheduleItem = await _context.ScheduleItems
                .Include(item => item.Seats)
                .Include(item => item.Movie)
                .Include(item => item.Room).ThenInclude(room => room.Seats)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (ScheduleItem == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}