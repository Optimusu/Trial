using Mapster;
using Trial.Domain.Entities;
using Trial.Domain.EntitiesStudy;

namespace Trial.AppInfra.Mappings;

public static class MapsterConfig
{
    public static void RegisterMappings()
    {
        var config = TypeAdapterConfig.GlobalSettings;

        config.NewConfig<Manager, Manager>()
             .Ignore(dest => dest.Corporation!);

        config.NewConfig<Study, Study>()
            .Ignore(dest => dest.EdocCategories!)
            .Ignore(dest => dest.Corporation!)
            .Ignore(dest => dest.TherapeuticArea!)
            .Ignore(dest => dest.Enrolling!)
            .Ignore(dest => dest.Indication!)
            .Ignore(dest => dest.Sponsor!)
            .Ignore(dest => dest.Usuario!)
            .Ignore(dest => dest.Irb!)
            .Ignore(dest => dest.Cro!);
    }
}