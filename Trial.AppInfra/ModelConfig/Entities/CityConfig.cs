using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trial.Domain.Entities;

namespace Trial.AppInfra.ModelConfig.Entities;

public class CityConfig : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.HasIndex(e => e.CityId);
        builder.HasIndex(e => new { e.Name, e.StateId }).IsUnique();
        //Proteccion de Borrado en Cascada
        builder.HasOne(e => e.State).WithMany(e => e.Cities).OnDelete(DeleteBehavior.Restrict);
    }
}