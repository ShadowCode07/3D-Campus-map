using System;
using System.Collections.Generic;

namespace CampusMapAPI.Models;

public class Building : Base
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Scene> Scenes { get; set; } = new List<Scene>();
}
