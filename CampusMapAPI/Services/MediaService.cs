using CampusMapAPI.Dtos.Media;
using CampusMapAPI.Interfaces.IRepositories;
using CampusMapAPI.Interfaces.Services;
using CampusMapAPI.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace CampusMapAPI.Services
{
    public class MediaService : IMediaService
    {
        private readonly IMediaRepository _MediaRepository;

        public MediaService(IMediaRepository MediaRepository)
        {
            _MediaRepository = MediaRepository;
        }

        public async Task<MediaResponseDto> CreateAsync(MediaCreateDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var Media = dto.Adapt<Media>();

            await _MediaRepository.CreateAsync(Media);
            await _MediaRepository.SaveChanges();

            return Media.Adapt<MediaResponseDto>();
        }

        public async Task<IEnumerable<MediaResponseDto>> GetAllAsync()
        {
            var Medias = await _MediaRepository.GetAllAsync();
            return Medias.Adapt<IEnumerable<MediaResponseDto>>();
        }

        public async Task<MediaResponseDto?> GetByIdAsync(int id)
        {
            var Media = await _MediaRepository.GetByIdAsync(id);
            return Media?.Adapt<MediaResponseDto>();
        }

        public async Task<MediaResponseDto?> UpdateAsync(int id, MediaUpdateDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var existing = await _MediaRepository.GetByIdAsync(id);
            if (existing is null)
            {
                return null;
            }

            dto.Adapt(existing);

            try
            {
                await _MediaRepository.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }

            return existing.Adapt<MediaResponseDto>();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var deleted = await _MediaRepository.DeleteByIdAsync(id);
            if (!deleted)
            {
                return false;
            }

            await _MediaRepository.SaveChanges();
            return true;
        }
    }
}
