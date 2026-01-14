using CampusMapAPI.Data;
using CampusMapAPI.Interfaces.IRepositories;
using CampusMapAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CampusMapAPI.Repositories
{
    public class SceneRepository : Repository<Scene>, ISceneRepository
    {
        public SceneRepository(ApplicationDbContext context) : base(context)
        {
        }


        public async Task<IEnumerable<Scene>> GetByBuildingIdAsync(int buildingId)
        {
            return await _context.Scenes
                .Where(s => s.BuildingId == buildingId)
                .ToListAsync();
        }
    }

}
