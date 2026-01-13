using CampusMapAPI.Dtos.Media;
using CampusMapAPI.Interfaces.IRepositories;
using CampusMapAPI.Interfaces.Services;
using CampusMapAPI.Models;
using CampusMapAPI.Repositories;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace CampusMapAPI.Services
{
    public class MediaService : IMediaService
    {
        private readonly IMediaRepository _mediaRepository;

        public MediaService(IMediaRepository mediaRepository)
        {
            _mediaRepository = mediaRepository;
        }

        public async Task<MediaResponseDto> CreateAsync(MediaCreateDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var Media = dto.Adapt<Media>();

            await _mediaRepository.CreateAsync(Media);
            await _mediaRepository.SaveChanges();

            return Media.Adapt<MediaResponseDto>();
        }

        public async Task<IEnumerable<MediaResponseDto>> GetAllAsync()
        {
            var Medias = await _mediaRepository.GetAllAsync();
            return Medias.Adapt<IEnumerable<MediaResponseDto>>();
        }

        public async Task<MediaResponseDto?> GetByIdAsync(int id)
        {
            var Media = await _mediaRepository.GetByIdAsync(id);
            return Media?.Adapt<MediaResponseDto>();
        }

        public async Task<MediaResponseDto?> UpdateAsync(int id, MediaUpdateDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var existing = await _mediaRepository.GetByIdAsync(id);
            if (existing is null)
            {
                return null;
            }

            dto.Adapt(existing);

            try
            {
                await _mediaRepository.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }

            return existing.Adapt<MediaResponseDto>();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var deleted = await _mediaRepository.DeleteByIdAsync(id);
            if (!deleted)
            {
                return false;
            }

            await _mediaRepository.SaveChanges();
            return true;
        }
        public async Task<IEnumerable<MediaResponseDto>> GetByIdsAsync(int[] mediaIds)
        {
            var medias = await _mediaRepository.GetByIdsAsync(mediaIds);

            return medias.Adapt<IEnumerable<MediaResponseDto>>();
        }
    }
}
