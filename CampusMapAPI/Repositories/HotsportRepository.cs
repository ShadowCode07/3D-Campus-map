using CampusMapAPI.Data;
using CampusMapAPI.Interfaces.IRepositories;
using CampusMapAPI.Models;

namespace CampusMapAPI.Repositories
{
    public class HotsportRepository : Repository<Hotspot>, IHotspotRepository
    {
        public HotsportRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
