using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trial.Domain.EntitiesGen;

namespace Trial.AppInfra.ModelConfig.EntitiesGen;

public class TherapeuticAreaConfig : IEntityTypeConfiguration<TherapeuticArea>
{
    public void Configure(EntityTypeBuilder<TherapeuticArea> builder)
    {
        builder.HasKey(e => e.TherapeuticAreaId);
        builder.HasIndex(e => new { e.Name }).IsUnique();
        builder.Property(e => e.Name).UseCollation("Latin1_General_CI_AS"); //Para poderlo volver Collation CI
    }
}