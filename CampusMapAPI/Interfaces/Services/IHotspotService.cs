using CampusMapAPI.Dtos.Hotspot;
using CampusMapAPI.Models;

namespace CampusMapAPI.Interfaces.Services
{
    public interface IHotspotService
    {
        Task<HotspotResponseDto> CreateAsync(HotspotCreateDto dto);
        Task<HotspotResponseDto?> UpdateAsync(int id, HotspotUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<HotspotResponseDto?> GetByIdAsync(int id);
        Task<IEnumerable<HotspotResponseDto>> GetAllAsync();
    }
}
