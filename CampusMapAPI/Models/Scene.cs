using System;
using System.Collections.Generic;

namespace CampusMapAPI.Models;

public class Scene : Base
{

    public int? BuildingId { get; set; }

    public int? PreviewMediaId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal? StartHfov { get; set; }

    public string? Scenecol { get; set; }

    public Building? Building { get; set; }

    public ICollection<Hotspot> Hotspots { get; set; } = new List<Hotspot>();

    public Media? PreviewMedia { get; set; }
}
