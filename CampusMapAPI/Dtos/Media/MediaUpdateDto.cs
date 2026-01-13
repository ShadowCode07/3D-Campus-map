namespace CampusMapAPI.Dtos.Media
{
    public class MediaUpdateDto
    {
        public string? Url { get; set; }
        public string? MediaType { get; set; }
        public string? Title { get; set; }
        public List<int>? HotspotIds { get; set; }
        public List<int>? SceneIds { get; set; }
    }
}
