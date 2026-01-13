using CampusMapAPI.Data;
using CampusMapAPI.Interfaces.IRepositories;
using CampusMapAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CampusMapAPI.Repositories
{
    public class HotsportRepository : Repository<Hotspot>, IHotspotRepository
    {
        public HotsportRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Hotspot>> GetBySceneIdsAsync(int[] sceneIds)
        {
            return await _context.Hotspots
                .Where(h => h.SceneId.HasValue && sceneIds.Contains(h.SceneId.Value))
                .ToListAsync();
        }
    }
}
