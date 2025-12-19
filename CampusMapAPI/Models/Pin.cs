namespace CampusMapAPI.Models
{
    public class Pin
    {
        public int Pin_ID { get; set; }
        public int Building_ID { get; set; } 
        public int? Media_ID { get; set; }      
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Location_Type { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? X { get; set; }
        public decimal? Y { get; set; }
        public decimal? Z { get; set; }
        public string? Icon_Type { get; set; }
    }
}
