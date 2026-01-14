using CampusMapAPI.Dtos.Building;

namespace CampusMapAPI.Interfaces.Services;

public interface IBuildingService
{
    Task<IEnumerable<BuildingResponseDto>> GetAllAsync();
    Task<BuildingResponseDto?> GetByIdAsync(int id);
    Task<BuildingResponseDto> CreateAsync(BuildingCreateDto dto);
    Task<BuildingResponseDto?> UpdateAsync(int id, BuildingUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}

