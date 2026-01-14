using CampusMapAPI.Dtos.Building;
using CampusMapAPI.Interfaces.IRepositories;
using CampusMapAPI.Interfaces.Services;
using CampusMapAPI.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace CampusMapAPI.Services;

public class BuildingService : IBuildingService
{
    private readonly IBuildingRepository _buildingRepository;

    public BuildingService(IBuildingRepository buildingRepository)
    {
        _buildingRepository = buildingRepository;
    }

    public async Task<BuildingResponseDto> CreateAsync(BuildingCreateDto dto)
    {
        var building = dto.Adapt<Building>();
        await _buildingRepository.CreateAsync(building);
        await _buildingRepository.SaveChanges();
        return building.Adapt<BuildingResponseDto>();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var deleted = await _buildingRepository.DeleteByIdAsync(id);
        if (!deleted) return false;

        await _buildingRepository.SaveChanges();
        return true;
    }

    public async Task<IEnumerable<BuildingResponseDto>> GetAllAsync()
    {
        var buildings = await _buildingRepository.GetAllAsync();
        return buildings.Adapt<IEnumerable<BuildingResponseDto>>();
    }

    public async Task<BuildingResponseDto?> GetByIdAsync(int id)
    {
        var building = await _buildingRepository.GetByIdAsync(id);
        return building?.Adapt<BuildingResponseDto>();
    }

    public async Task<BuildingResponseDto?> UpdateAsync(int id, BuildingUpdateDto dto)
    {
        var existing = await _buildingRepository.GetByIdAsync(id);
        if (existing is null) return null;

        dto.Adapt(existing);
        await _buildingRepository.SaveChanges();

        return existing.Adapt<BuildingResponseDto>();
    }
}

