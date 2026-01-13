using CampusMapAPI.Dtos.Hotspot;
using CampusMapAPI.Dtos.Media;
using CampusMapAPI.Dtos.Pannellum;
using CampusMapAPI.Dtos.Scene;
using CampusMapAPI.Interfaces.IRepositories;
using CampusMapAPI.Interfaces.Services;
using CampusMapAPI.Models;

namespace CampusMapAPI.Services
{
    public class PannellumService : IPannellumService
    {
        private readonly ISceneService _sceneService;
        private readonly IHotspotService _hotspotService;
        private readonly IMediaService _mediaService;

        public PannellumService(ISceneService sceneService, IHotspotService hotspotService,
            IMediaService mediaService)
        {
            _sceneService = sceneService;
            _hotspotService = hotspotService;
            _mediaService = mediaService;
        }

        public Task<PannellumConfigDto> GetBuildingTourAsync(int buildingId)
           => GetBuildingTourInternalAsync(buildingId, null);

        public Task<PannellumConfigDto> GetBuildingTourAsync(int buildingId, string firstSceneId)
            => GetBuildingTourInternalAsync(buildingId, firstSceneId);

        private async Task<PannellumConfigDto> GetBuildingTourInternalAsync(int buildingId, string? firstSceneId)
        {
            var scenes = (await _sceneService.GetByBuildingIdAsync(buildingId))?.ToList()
                         ?? new List<SceneResponseDto>();

            if (scenes.Count == 0)
                throw new KeyNotFoundException($"No scenes found for buildingId={buildingId}.");

            var validScenes = scenes.Where(s => !string.IsNullOrWhiteSpace(s.Scenecol)).ToList();
            if (validScenes.Count == 0)
                throw new InvalidOperationException("Scenes exist but none have Scenecol set.");

            var sceneIds = validScenes.Select(s => s.Id).ToArray();

            var hotspots = (await _hotspotService.GetBySceneIdsAsync(sceneIds))?.ToList()
                           ?? new List<HotspotResponseDto>();

            var mediaIds = validScenes
                .Where(s => s.PreviewMediaId.HasValue)
                .Select(s => s.PreviewMediaId!.Value)
                .Concat(hotspots.Where(h => h.MediaId.HasValue).Select(h => h.MediaId!.Value))
                .Distinct()
                .ToArray();

            var media = mediaIds.Length == 0
                ? new List<MediaResponseDto>()
                : ((await _mediaService.GetByIdsAsync(mediaIds))?.ToList() ?? new List<MediaResponseDto>());

            var mediaById = media.ToDictionary(m => m.Id, m => m);

            var sceneKeyById = validScenes.ToDictionary(s => s.Id, s => s.Scenecol!);

            var first = !string.IsNullOrWhiteSpace(firstSceneId) ? firstSceneId! : validScenes[0].Scenecol!;
            if (!validScenes.Any(s => s.Scenecol == first))
                first = validScenes[0].Scenecol!;

            var hotspotsBySceneId = hotspots
                .Where(h => h.SceneId.HasValue)
                .GroupBy(h => h.SceneId!.Value)
                .ToDictionary(g => g.Key, g => g.ToList());

            var config = new PannellumConfigDto
            {
                Default = new PannellumDefaultDto { FirstScene = first },
                Scenes = new Dictionary<string, PannellumSceneDto>()
            };

            foreach (var scene in validScenes)
            {
                var panoUrl = scene.PreviewMediaId.HasValue &&
                              mediaById.TryGetValue(scene.PreviewMediaId.Value, out var panoMedia)
                    ? panoMedia.Url
                    : string.Empty;

                var panoType = scene.PreviewMediaId.HasValue &&
                               mediaById.TryGetValue(scene.PreviewMediaId.Value, out var typeMedia)
                    ? NormalizePanoType(typeMedia.MediaType)
                    : "equirectangular";

                var sceneHotspots = hotspotsBySceneId.TryGetValue(scene.Id, out var hs)
                    ? hs
                    : new List<HotspotResponseDto>();

                config.Scenes[scene.Scenecol!] = new PannellumSceneDto
                {
                    Type = panoType,
                    Panorama = panoUrl,
                    Pitch = scene.StartPitch,
                    Yaw = scene.StartYaw,
                    Hfov = scene.StartHfov,
                    Title = scene.Name,
                    HotSpots = sceneHotspots
                        .Select(h => MapHotspot(h, sceneKeyById))
                        .Where(h => h != null)
                        .Select(h => h!)
                        .ToList()
                };
            }

            foreach (var sc in config.Scenes.Values)
            {
                sc.HotSpots = sc.HotSpots
                    .Where(h => !string.Equals(h.Type, "scene", StringComparison.OrdinalIgnoreCase)
                                || (h.SceneId != null && config.Scenes.ContainsKey(h.SceneId)))
                    .ToList();
            }

            return config;
        }

        private static PannellumHotSpotDto? MapHotspot(HotspotResponseDto h, Dictionary<int, string> sceneKeyById)
        {
            var type = string.IsNullOrWhiteSpace(h.Type) ? "scene" : h.Type.ToLowerInvariant();

            var dto = new PannellumHotSpotDto
            {
                Pitch = h.Pitch ?? 0,
                Yaw = h.Yaw ?? 0,
                Type = type,
                Text = h.Text
            };

            if (type == "scene" &&
                h.TargetSceneId.HasValue &&
                sceneKeyById.TryGetValue(h.TargetSceneId.Value, out var targetKey))
            {
                dto.SceneId = targetKey;
            }

            return dto;
        }

        private static string NormalizePanoType(string? mediaType)
        {
            if (string.IsNullOrWhiteSpace(mediaType))
                return "equirectangular";

            return mediaType.ToLowerInvariant() switch
            {
                "cubemap" => "cubemap",
                "multires" => "multires",
                _ => "equirectangular"
            };
        }
    }
}
