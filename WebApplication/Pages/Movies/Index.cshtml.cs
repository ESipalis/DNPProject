using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Data.Entities;

namespace WebApplication.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IList<Movie> Movies { get;set; }

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Movies = await _context.Movies.ToListAsync();
        }
    }
}
