using CampusMapAPI.Dtos.Hotspot;
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
        }
    }

}
