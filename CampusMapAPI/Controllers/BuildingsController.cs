using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CampusMapAPI.Data;
using CampusMapAPI.Models;


namespace CampusMapAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuildingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BuildingsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/Buildings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Building>>> GetBuildings()
        {
            return await _context.Buildings.ToListAsync();
        }
    }
}
