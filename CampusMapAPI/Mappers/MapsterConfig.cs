using CampusMapAPI.Dtos.Hotspot;
using CampusMapAPI.Dtos.Media;
using CampusMapAPI.Dtos.Scene;
using CampusMapAPI.Models;
using Mapster;

namespace CampusMapAPI.Mappers
{
    public static class MapsterConfig
    {
        public static void RegisterMapsterConfiguration(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;

            config.NewConfig<Hotspot, HotspotResponseDto>()
                .Map(dest => dest.ConcurrencyToken, src => Convert.ToBase64String(src.Version));

            config.NewConfig<HotspotCreateDto, Hotspot>()
                .Ignore(dest => dest.Version)
                .Ignore(dest => dest.Media)
                .Ignore(dest => dest.Scene)
                .IgnoreNullValues(true);

            config.NewConfig<HotspotUpdateDto, Hotspot>()
                .Ignore(dest => dest.Version)
                .Ignore(dest => dest.Media)
                .Ignore(dest => dest.Scene)
                .IgnoreNullValues(true);

            config.NewConfig<Scene, SceneResponseDto>()
                .Map(dest => dest.ConcurrencyToken, src => Convert.ToBase64String(src.Version));

            config.NewConfig<SceneCreateDto, Scene>()
                .Ignore(dest => dest.Version)
                .Ignore(dest => dest.Building)
                .Ignore(dest => dest.PreviewMedia)
                .Ignore(dest => dest.Hotspots)
                .IgnoreNullValues(true);

            config.NewConfig<SceneUpdateDto, Scene>()
                .Ignore(dest => dest.Version)
                .Ignore(dest => dest.Building)
                .Ignore(dest => dest.PreviewMedia)
                .Ignore(dest => dest.Hotspots)
                .IgnoreNullValues(true);

            config.NewConfig<Media, MediaResponseDto>()
                .Map(dest => dest.ConcurrencyToken, src => Convert.ToBase64String(src.Version));

            config.NewConfig<MediaCreateDto, Media>()
                .Ignore(dest => dest.Version)
                .Ignore(dest => dest.Hotspots)
                .Ignore(dest => dest.Scenes)
                .IgnoreNullValues(true);

            config.NewConfig<MediaUpdateDto, Media>()
                .Ignore(dest => dest.Version)
                .Ignore(dest => dest.Hotspots)
                .Ignore(dest => dest.Scenes)
                .IgnoreNullValues(true);
        }
    }
}

