using CampusMapAPI.Models;

namespace CampusMapAPI.Interfaces.IRepositories
{
    public interface IMediaRepository : IRepository<Media>
    {
        Task<IEnumerable<Media>> GetByIdsAsync(int[] mediaIds);
    }
}
