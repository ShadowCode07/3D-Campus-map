using CampusMapAPI.Dtos.Pannellum;

namespace CampusMapAPI.Interfaces.Services
{
    public interface IPannellumService
    {
        Task<PannellumConfigDto> GetBuildingTourAsync(int buildingId);
        Task<PannellumConfigDto> GetBuildingTourAsync(
        int buildingId,
        string firstSceneId
    );
    }
}
