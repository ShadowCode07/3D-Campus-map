using CampusMapAPI.Dtos.Scene;

namespace CampusMapAPI.Dtos.Building;

public class BuildingResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public List<SceneResponseDto> Scenes { get; set; } = new();
}
