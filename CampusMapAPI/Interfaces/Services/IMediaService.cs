using CampusMapAPI.Dtos.Media;
using CampusMapAPI.Models;

namespace CampusMapAPI.Interfaces.Services
{
    public interface IMediaService
    {
        Task<MediaResponseDto> CreateAsync(MediaCreateDto dto);
        Task<MediaResponseDto?> UpdateAsync(int id, MediaUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<MediaResponseDto?> GetByIdAsync(int id);
        Task<IEnumerable<MediaResponseDto>> GetAllAsync();
        Task<IEnumerable<MediaResponseDto>> GetByIdsAsync(int[] mediaIds);
    }
}
