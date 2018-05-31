using Microsoft.AspNetCore.Mvc;
using WebApplication.Data;

namespace WebApplication.Controllers
{
    [Route("/api/movies")]
    public class MoviesApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MoviesApiController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public IActionResult GetAll()
        {
            return Ok(_context.Movies);
        }

        [HttpGet("{id}")]
        public IActionResult Index(int id)
        {
            var movie = _context.Movies.Find(id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }
    }
}