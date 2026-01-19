namespace CampusMapAPI.Dtos.Hotspot
{
    public class HotspotUpdateDto
    {
        public int? SceneId { get; set; }
        public int? MediaId { get; set; }
        public int? TargetSceneId { get; set; }

        public string? Type { get; set; }
        public decimal? Pitch { get; set; }
        public decimal? Yaw { get; set; }
        public decimal? TargetPitch { get; set; }
        public decimal? TargetYaw { get; set; }
        public string? Url { get; set; }
        public string? Name { get; set; }
        public string? Text { get; set; }
        public string? IconType { get; set; }
    }
}
