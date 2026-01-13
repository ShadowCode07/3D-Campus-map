using System.Text.Json.Serialization;

namespace CampusMapAPI.Dtos.Pannellum
{
    public class PannellumConfigDto
    {
        [JsonPropertyName("default")]
        public PannellumDefaultDto Default { get; set; } = new();

        [JsonPropertyName("scenes")]
        public Dictionary<string, PannellumSceneDto> Scenes { get; set; } = new();
    }
}
