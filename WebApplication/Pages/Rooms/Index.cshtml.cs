using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Data.Entities;

namespace WebApplication.Pages.Rooms
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IList<Room> Rooms { get;set; }

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Rooms = await _context.Rooms.Include(room => room.Seats)
                .ToListAsync();
        }
    }
}
