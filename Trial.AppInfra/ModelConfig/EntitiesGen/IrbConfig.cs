using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trial.Domain.EntitiesGen;

namespace Trial.AppInfra.ModelConfig.EntitiesGen;

public class IrbConfig : IEntityTypeConfiguration<Irb>
{
    public void Configure(EntityTypeBuilder<Irb> builder)
    {
        builder.HasKey(e => e.IrbId);
        builder.HasIndex(e => new { e.Name }).IsUnique();
        builder.Property(e => e.Name).UseCollation("Latin1_General_CI_AS"); //Para poderlo volver Collation CI
    }
}