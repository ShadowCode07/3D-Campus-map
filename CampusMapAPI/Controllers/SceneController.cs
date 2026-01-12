using CampusMapAPI.Dtos.Scene;
using CampusMapAPI.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CampusMapAPI.Controllers
{
    [ApiController]
    [Route("api/scene")]
    public class SceneController : ControllerBase   
    {
        private readonly ISceneService _sceneService;

        public SceneController(ISceneService sceneService)
        {
            _sceneService = sceneService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SceneResponseDto>>> GetAll()
        {
            var scenes = await _sceneService.GetAllAsync();
            return Ok(scenes);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SceneResponseDto>> GetById([FromRoute] int id)
        {
            var scene = await _sceneService.GetByIdAsync(id);
            if (scene is null)
            {
                return NotFound();
            }

            return Ok(scene);
        }

        [HttpPost]
        public async Task<ActionResult<SceneResponseDto>> Create([FromBody] SceneCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = await _sceneService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                created
            );
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<SceneResponseDto>> Update(
            [FromRoute] int id,
            [FromBody] SceneUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = await _sceneService.UpdateAsync(id, dto);
            if (updated is null)
            {
                return NotFound();
            }

            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var deleted = await _sceneService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

