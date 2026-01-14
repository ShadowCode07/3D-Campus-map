using CampusMapAPI.Models;

namespace CampusMapAPI.Interfaces.IRepositories
{
    public interface ISceneRepository : IRepository<Scene>
    {
        Task<IEnumerable<Scene>> GetByBuildingIdAsync(int buildingId);
    }
}
