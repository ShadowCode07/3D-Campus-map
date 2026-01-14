using System;
using System.Collections.Generic;

namespace CampusMapAPI.Models;

public class Hotspot : Base
{
    public int? SceneId { get; set; }

    public int? MediaId { get; set; }

    public int? TargetSceneId { get; set; }

    public string? Type { get; set; }

    public decimal? Pitch { get; set; }

    public decimal? Yaw { get; set; }
    public decimal? TargetPitch { get; set; }

    public decimal? TargetYaw { get; set; }

    public string? Name { get; set; }

    public string? Text { get; set; }

    public string? IconType { get; set; }

    public Media? Media { get; set; }

    public Scene? Scene { get; set; }
}
