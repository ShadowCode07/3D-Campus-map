using CampusMapAPI.Dtos.Hotspot;
using CampusMapAPI.Interfaces.IRepositories;
using CampusMapAPI.Interfaces.Services;
using CampusMapAPI.Models;
using Mapster;
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

        public async Task<HotspotResponseDto> CreateAsync(HotspotCreateDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var hotspot = dto.Adapt<Hotspot>();

            await _hotspotRepository.CreateAsync(hotspot);
            await _hotspotRepository.SaveChanges();

            return hotspot.Adapt<HotspotResponseDto>();
        }

        public async Task<IEnumerable<HotspotResponseDto>> GetAllAsync()
        {
            var hotspots = await _hotspotRepository.GetAllAsync();
            return hotspots.Adapt<IEnumerable<HotspotResponseDto>>();
        }

        public async Task<HotspotResponseDto?> GetByIdAsync(int id)
        {
            var hotspot = await _hotspotRepository.GetByIdAsync(id);
            return hotspot?.Adapt<HotspotResponseDto>();
        }

        public async Task<HotspotResponseDto?> UpdateAsync(int id, HotspotUpdateDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var existing = await _hotspotRepository.GetByIdAsync(id);
            if (existing is null)
            {
                return null;
            }

            dto.Adapt(existing);

            try
            {
                await _hotspotRepository.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }

            return existing.Adapt<HotspotResponseDto>();
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
