using CampusMapAPI.Dtos.Hotspot;
using CampusMapAPI.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CampusMapAPI.Controllers
{
    [ApiController]
    [Route("api/hotspot")]
    public class HotspotsController : ControllerBase
    {
        private readonly IHotspotService _hotspotService;

        public HotspotsController(IHotspotService hotspotService)
        {
            _hotspotService = hotspotService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotspotResponseDto>>> GetAll()
        {
            var hotspots = await _hotspotService.GetAllAsync();
            return Ok(hotspots);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<HotspotResponseDto>> GetById([FromRoute] int id)
        {
            var hotspot = await _hotspotService.GetByIdAsync(id);
            if (hotspot is null)
            {
                return NotFound();
            }

            return Ok(hotspot);
        }

        [HttpPost]
        public async Task<ActionResult<HotspotResponseDto>> Create([FromBody] HotspotCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = await _hotspotService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                created
            );
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<HotspotResponseDto>> Update(
            [FromRoute] int id,
            [FromBody] HotspotUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = await _hotspotService.UpdateAsync(id, dto);
            if (updated is null)
            {
                return NotFound();
            }

            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var deleted = await _hotspotService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
