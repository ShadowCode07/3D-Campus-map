using System.Text.Json.Serialization;

namespace CampusMapAPI.Dtos.Pannellum
{
    public class PannellumDefaultDto
    {
        [JsonPropertyName("firstScene")]
        public string FirstScene { get; set; } = "";
    }
}
