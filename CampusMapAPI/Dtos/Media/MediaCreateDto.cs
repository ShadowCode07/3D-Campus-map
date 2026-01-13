namespace CampusMapAPI.Dtos.Media
{
    public class MediaCreateDto
    {
        public string Url { get; set; } = null!;
        public string? MediaType { get; set; }
        public string? Title { get; set; }
        public List<int>? HotspotIds { get; set; }
        public List<int>? SceneIds { get; set; }
    }
}
