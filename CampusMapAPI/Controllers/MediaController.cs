using CampusMapAPI.Dtos.Media;
using CampusMapAPI.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CampusMapAPI.Controllers
{
    [ApiController]
    [Route("api/media")]
    public class MediasController : ControllerBase
    {
        private readonly IMediaService _MediaService;

        public MediasController(IMediaService MediaService)
        {
            _MediaService = MediaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MediaResponseDto>>> GetAll()
        {
            var Medias = await _MediaService.GetAllAsync();
            return Ok(Medias);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MediaResponseDto>> GetById([FromRoute] int id)
        {
            var Media = await _MediaService.GetByIdAsync(id);
            if (Media is null)
            {
                return NotFound();
            }

            return Ok(Media);
        }

        [HttpPost]
        public async Task<ActionResult<MediaResponseDto>> Create([FromBody] MediaCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = await _MediaService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                created
            );
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<MediaResponseDto>> Update(
            [FromRoute] int id,
            [FromBody] MediaUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = await _MediaService.UpdateAsync(id, dto);
            if (updated is null)
            {
                return NotFound();
            }

            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var deleted = await _MediaService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
