using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Data.Entities;

namespace WebApplication.Pages.ScheduleItems
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IList<ScheduleItem> ScheduleItems { get;set; }

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task OnGetAsync()
        {
            ScheduleItems = await _context.ScheduleItems
                .Include(item => item.Movie)
                .Include(item => item.Room)
                .Include(item => item.Seats)
                .ToListAsync();
        }
    }
}
