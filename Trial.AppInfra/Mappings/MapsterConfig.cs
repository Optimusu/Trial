using Mapster;
using Trial.Domain.Entities;

namespace Trial.AppInfra.Mappings;

public static class MapsterConfig
{
    public static void RegisterMappings()
    {
        var config = TypeAdapterConfig.GlobalSettings;

        config.NewConfig<Manager, Manager>()
              .Ignore(dest => dest.Corporation!);
    }
}