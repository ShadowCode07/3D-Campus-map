using CampusMapAPI.Interfaces.IRepositories;
using CampusMapAPI.Interfaces.Services;
using CampusMapAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CampusMapAPI.Services
{
    public class HotspotService : IHotspotService
    {
        private readonly IHotspotRepository _hotspotRepository;

        public HotspotService(IHotspotRepository hotspotRepository)
        {
            _hotspotRepository = hotspotRepository;
        }

        public async Task<Hotspot> CreateAsync(Hotspot hotspot)
        {

            await _hotspotRepository.CreateAsync(hotspot);
            await _hotspotRepository.SaveChanges();

            return hotspot;
        }

        public async Task<IEnumerable<Hotspot>> GetAllAsync()
        {
            return await _hotspotRepository.GetAllAsync();
        }

        public async Task<Hotspot?> GetByIdAsync(int id)
        {
            return await _hotspotRepository.GetByIdAsync(id);
        }

        public async Task<Hotspot?> UpdateAsync(int id, Hotspot hotspot)
        {
            if (hotspot == null)
            {
                throw new ArgumentNullException(nameof(hotspot));
            }

            var existing = await _hotspotRepository.GetByIdAsync(id);
            if (existing is null)
            {
                return null;
            }

            try
            {
                await _hotspotRepository.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }

            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var deleted = await _hotspotRepository.DeleteByIdAsync(id);
            if (!deleted)
            {
                return false;
            }

            await _hotspotRepository.SaveChanges();
            return true;
        }
    }
}
