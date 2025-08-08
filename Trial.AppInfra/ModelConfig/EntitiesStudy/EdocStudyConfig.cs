using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trial.Domain.EntitiesStudy;

namespace Trial.AppInfra.ModelConfig.EntitiesStudy;

public class EdocStudyConfig : IEntityTypeConfiguration<EdocStudy>
{
    public void Configure(EntityTypeBuilder<EdocStudy> builder)
    {
        builder.HasIndex(e => e.EdocStudyId);
        builder.Property(x => x.EdocStudyId).HasDefaultValueSql("NEWSEQUENTIALID()");
        builder.HasIndex(e => new { e.FileNameOriginal, e.CorporationId }).IsUnique();
        //Proteccion de Borrado en Cascada
        builder.HasOne(e => e.EdocCategory).WithMany(e => e.EdocStudies).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(e => e.Study).WithMany(e => e.EdocStudies).OnDelete(DeleteBehavior.Restrict);
    }
}