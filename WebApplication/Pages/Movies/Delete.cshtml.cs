using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Data.Entities;

namespace WebApplication.Pages.Movies
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        [BindProperty] public Movie Movie { get; set; }

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie = await _context.Movies.SingleOrDefaultAsync(m => m.Id == id);

            if (Movie == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie = await _context.Movies.FindAsync(id);

            if (Movie != null)
            {
                _context.Movies.Remove(Movie);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}