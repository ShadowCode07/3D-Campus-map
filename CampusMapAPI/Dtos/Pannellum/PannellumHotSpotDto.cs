using System.Text.Json.Serialization;

namespace CampusMapAPI.Dtos.Pannellum
{
    public class PannellumHotSpotDto
    {
        [JsonPropertyName("pitch")]
        public decimal Pitch { get; set; }

        [JsonPropertyName("yaw")]
        public decimal Yaw { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; } = "scene";

        [JsonPropertyName("sceneId")]
        public string? SceneId { get; set; }

        [JsonPropertyName("text")]
        public string? Text { get; set; }

        [JsonPropertyName("URL")]
        public string? Url { get; set; }

        [JsonPropertyName("targetPitch")]
        public decimal? TargetPitch { get; set; }
        [JsonPropertyName("targetYaw")]
        public decimal? TargetYaw { get; set; }
    }
}
