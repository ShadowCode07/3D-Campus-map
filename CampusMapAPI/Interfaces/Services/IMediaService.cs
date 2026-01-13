using CampusMapAPI.Dtos.Media;

namespace CampusMapAPI.Interfaces.Services
{
    public interface IMediaService
    {
        Task<MediaResponseDto> CreateAsync(MediaCreateDto dto);
        Task<MediaResponseDto?> UpdateAsync(int id, MediaUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<MediaResponseDto?> GetByIdAsync(int id);
        Task<IEnumerable<MediaResponseDto>> GetAllAsync();
    }
}
