using CampusMapAPI.Data;
using CampusMapAPI.Interfaces.IRepositories;
using CampusMapAPI.Models;

namespace CampusMapAPI.Repositories
{
    public class SceneRepository : Repository<Scene>, ISceneRepository
    {
        public SceneRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
