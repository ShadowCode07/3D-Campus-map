namespace CampusMapAPI.Dtos.Media
{
    public class MediaCreateDto
    {
        public string Url { get; set; } = null!;
        public string? MediaType { get; set; }
        public string? Title { get; set; }
        public List<Guid>? HotspotIds { get; set; }
        public List<Guid>? SceneIds { get; set; }
    }
}
