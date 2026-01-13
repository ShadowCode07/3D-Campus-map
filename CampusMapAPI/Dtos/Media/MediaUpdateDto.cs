namespace CampusMapAPI.Dtos.Media
{
    public class MediaUpdateDto
    {
        public string? Url { get; set; }
        public string? MediaType { get; set; }
        public string? Title { get; set; }

        // If you want to update relationships by IDs (common approach)
        public List<Guid>? HotspotIds { get; set; }
        public List<Guid>? SceneIds { get; set; }
    }
}
