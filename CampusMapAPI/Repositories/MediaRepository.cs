using CampusMapAPI.Data;
using CampusMapAPI.Interfaces.IRepositories;
using CampusMapAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CampusMapAPI.Repositories
{
    public class MediaRepository : Repository<Media>, IMediaRepository
    {
        public MediaRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Media>> GetByIdsAsync(int[] mediaIds)
        {
            return await _context.Media
                .Where(m => mediaIds.Contains(m.Id))
                .ToListAsync();
        }
    }
}
