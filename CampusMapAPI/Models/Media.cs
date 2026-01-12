using System;
using System.Collections.Generic;

namespace CampusMapAPI.Models;

public class Media : Base
{
    public string Url { get; set; } = null!;

    public string? MediaType { get; set; }

    public string? Title { get; set; }

    public ICollection<Hotspot> Hotspots { get; set; } = new List<Hotspot>();

    public ICollection<Scene> Scenes { get; set; } = new List<Scene>();
}
