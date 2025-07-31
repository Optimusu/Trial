using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trial.Domain.EntitiesGen;

namespace Trial.AppInfra.ModelConfig.EntitiesGen;

public class CroConfig : IEntityTypeConfiguration<Cro>
{
    public void Configure(EntityTypeBuilder<Cro> builder)
    {
        builder.HasKey(e => e.CroId);
        builder.HasIndex(e => new { e.Name }).IsUnique();
        builder.Property(e => e.Name).UseCollation("Latin1_General_CI_AS"); //Para poderlo volver Collation CI
    }
}