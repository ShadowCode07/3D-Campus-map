using System;
using System.Collections.Generic;

namespace CampusMapAPI.Models;

public class Scene : Base
{

    public int? BuildingId { get; set; }

    public int? PreviewMediaId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal? StartPitch { get; set; }

    public decimal? StartYaw { get; set; }

    public decimal? StartHfov { get; set; }

    public string? Scenecol { get; set; }

    public virtual Building? Building { get; set; }

    public virtual ICollection<Hotspot> Hotspots { get; set; } = new List<Hotspot>();

    public virtual Media? PreviewMedia { get; set; }
}
