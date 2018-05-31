using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Data.Entities;

namespace WebApplication.Controllers
{
    [Route("/api/schedule")]
    public class ScheduleApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ScheduleApiController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var schedule = await _context.ScheduleItems
                .Include(item => item.Movie)
                .Include(item => item.Room)
                .ToListAsync();
            return Ok(schedule);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Index(int id)
        {
            var scheduleItem = await _context.ScheduleItems
                .Include(item => item.Movie)
                .Include(item => item.Room)
                .FirstOrDefaultAsync(db => db.Id == id);
            if (scheduleItem == null)
            {
                return NotFound();
            }

            return Ok(scheduleItem);
        }
    }
}