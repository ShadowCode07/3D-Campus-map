using CampusMapAPI.Dtos.Hotspot;
using CampusMapAPI.Dtos.Scene;

namespace CampusMapAPI.Dtos.Media
{
    public class MediaResponseDto
    {
        public Guid Id { get; set; }
        public string Url { get; set; } = null!;
        public string? MediaType { get; set; }
        public string? Title { get; set; }
        public List<HotspotResponseDto> Hotspots { get; set; } = new();
        public List<SceneResponseDto> Scenes { get; set; } = new();
        public string ConcurrencyToken { get; set; } = string.Empty;
    }
}
