using CampusMapAPI.Data;
using CampusMapAPI.Interfaces.IRepositories;
using CampusMapAPI.Models;

namespace CampusMapAPI.Repositories
{
    public class MediaRepository : Repository<Media>, IMediaRepository
    {
        public MediaRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
