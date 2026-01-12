namespace CampusMapAPI.Dtos.Scene
{
    public class SceneResponseDto
    {
        public int Id { get; set; }                 
        public string? Name { get; set; }
        public int? BuildingId { get; set; }
        public int? PreviewMediaId { get; set; }
        public string? Description { get; set; }
        public decimal? StartPitch { get; set; }
        public decimal? StartYaw { get; set; }
        public decimal? StartHfov { get; set; }
        public string? Scenecol { get; set; }
    }
}
