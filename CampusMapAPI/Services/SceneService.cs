using CampusMapAPI.Dtos.Scene;
using CampusMapAPI.Interfaces.IRepositories;
using CampusMapAPI.Interfaces.Services;
using CampusMapAPI.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace CampusMapAPI.Services
{
    public class SceneService : ISceneService
    {
        private readonly ISceneRepository _sceneRepository;

        public SceneService(ISceneRepository sceneRepository)
        {
            _sceneRepository = sceneRepository;
        }

        public async Task<SceneResponseDto> CreateAsync(SceneCreateDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var scene = dto.Adapt<Scene>();

            await _sceneRepository.CreateAsync(scene);
            await _sceneRepository.SaveChanges();

            return scene.Adapt<SceneResponseDto>();
        }

        public async Task<IEnumerable<SceneResponseDto>> GetAllAsync()
        {
            var scenes = await _sceneRepository.GetAllAsync();
            return scenes.Adapt<IEnumerable<SceneResponseDto>>();
        }

        public async Task<SceneResponseDto?> GetByIdAsync(int id)
        {
            var scene = await _sceneRepository.GetByIdAsync(id);
            return scene?.Adapt<SceneResponseDto>();
        }

        public async Task<SceneResponseDto?> UpdateAsync(int id, SceneUpdateDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var existing = await _sceneRepository.GetByIdAsync(id);
            if (existing is null)
            {
                return null;
            }

            dto.Adapt(existing);

            try
            {
                await _sceneRepository.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }

            return existing.Adapt<SceneResponseDto>();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var deleted = await _sceneRepository.DeleteByIdAsync(id);
            if (!deleted)
            {
                return false;
            }

            await _sceneRepository.SaveChanges();
            return true;
        }
    }
}


