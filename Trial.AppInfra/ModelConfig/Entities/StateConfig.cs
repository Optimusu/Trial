using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trial.Domain.Entities;

namespace Trial.AppInfra.ModelConfig.Entities;

public class StateConfig : IEntityTypeConfiguration<State>
{
    public void Configure(EntityTypeBuilder<State> builder)
    {
        builder.HasIndex(e => e.StateId);
        builder.HasIndex(e => new { e.Name, e.CountryId }).IsUnique();
        //Proteccion de Borrado en Cascada
        builder.HasOne(e => e.Country).WithMany(e => e.States).OnDelete(DeleteBehavior.Restrict);
    }
}