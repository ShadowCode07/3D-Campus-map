namespace CampusMapAPI.Dtos.Scene
{
    public class SceneUpdateDto
    {
        public int? BuildingId { get; set; }
        public int? PreviewMediaId { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }

        public decimal? StartHfov { get; set; }

        public string? Scenecol { get; set; }

        public List<int>? HotspotIds { get; set; }
    }
}

