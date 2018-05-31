using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication.Data;
using WebApplication.Data.Entities;

namespace WebApplication.Pages.Movies
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        [BindProperty] public Movie Movie { get; set; }

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

            _context.Movies.Add(Movie);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}