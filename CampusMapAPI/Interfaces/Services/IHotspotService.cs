using CampusMapAPI.Models;

namespace CampusMapAPI.Interfaces.Services
{
    public interface IHotspotService
    {
        Task<Hotspot> CreateAsync(Hotspot dto);
        Task<Hotspot?> UpdateAsync(int id, Hotspot dto);
        Task<bool> DeleteAsync(int id);
        Task<Hotspot?> GetByIdAsync(int id);
        Task<IEnumerable<Hotspot>> GetAllAsync();
    }
}
