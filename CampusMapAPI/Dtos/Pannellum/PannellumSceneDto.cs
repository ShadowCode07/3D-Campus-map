using System.Text.Json.Serialization;

namespace CampusMapAPI.Dtos.Pannellum
{
    public class PannellumSceneDto
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = "equirectangular";

        [JsonPropertyName("panorama")]
        public string Panorama { get; set; } = "";

        [JsonPropertyName("pitch")]
        public decimal? Pitch { get; set; }

        [JsonPropertyName("yaw")]
        public decimal? Yaw { get; set; }

        [JsonPropertyName("hfov")]
        public decimal? Hfov { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("hotSpots")]
        public List<PannellumHotSpotDto> HotSpots { get; set; } = new();
    }
}
