using CampusMapAPI.Dtos.Scene;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampusMapAPI.Interfaces.Services
{
    public interface ISceneService
    {
        Task<IEnumerable<SceneResponseDto>> GetAllAsync();
        Task<SceneResponseDto?> GetByIdAsync(int id);
        Task<SceneResponseDto> CreateAsync(SceneCreateDto dto);
        Task<SceneResponseDto?> UpdateAsync(int id, SceneUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
