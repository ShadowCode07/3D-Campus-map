using CampusMapAPI.Dtos.Building;
using CampusMapAPI.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CampusMapAPI.Controllers;

[ApiController]
[Route("api/building")]
public class BuildingController : ControllerBase
{
    private readonly IBuildingService _buildingService;

    public BuildingController(IBuildingService buildingService)
    {
        _buildingService = buildingService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BuildingResponseDto>>> GetAll()
    {
        var buildings = await _buildingService.GetAllAsync();
        return Ok(buildings);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BuildingResponseDto>> GetById(int id)
    {
        var building = await _buildingService.GetByIdAsync(id);
        if (building is null) return NotFound();
        return Ok(building);
    }

    [HttpPost]
    public async Task<ActionResult<BuildingResponseDto>> Create([FromBody] BuildingCreateDto dto)
    {
        var created = await _buildingService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<BuildingResponseDto>> Update(int id, [FromBody] BuildingUpdateDto dto)
    {
        var updated = await _buildingService.UpdateAsync(id, dto);
        if (updated is null) return NotFound();
        return Ok(updated);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _buildingService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
