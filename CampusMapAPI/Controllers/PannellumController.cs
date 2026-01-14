using CampusMapAPI.Dtos.Pannellum;
using CampusMapAPI.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CampusMapAPI.Controllers
{

    [ApiController]
    [Route("api/buildings/{buildingId:int}/tour")]
    public class PannellumController : ControllerBase
    {
        private readonly IPannellumService _pannellumService;

        public PannellumController(IPannellumService pannellumService)
        {
            _pannellumService = pannellumService;
        }

        [HttpGet("pannellum")]
        public async Task<ActionResult<PannellumConfigDto>> GetPannellumConfig(
            [FromRoute] int buildingId,
            [FromQuery] string? firstScene = null)
        {
            try
            {
                PannellumConfigDto config = string.IsNullOrWhiteSpace(firstScene)
                    ? await _pannellumService.GetBuildingTourAsync(buildingId)
                    : await _pannellumService.GetBuildingTourAsync(buildingId, firstScene);

                return Ok(config);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }

}
