using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trial.Domain.EntitiesStudy;

namespace Trial.AppInfra.ModelConfig.EntitiesStudy;

public class EdocCategoryConfig : IEntityTypeConfiguration<EdocCategory>
{
    public void Configure(EntityTypeBuilder<EdocCategory> builder)
    {
        builder.HasIndex(e => e.EdocCategoryId);
        builder.Property(x => x.EdocCategoryId).HasDefaultValueSql("NEWSEQUENTIALID()");
        builder.HasIndex(e => new { e.Name, e.CorporationId }).IsUnique();
        builder.HasIndex(e => new { e.NameContainer }).IsUnique();
        //Proteccion de Borrado en Cascada
        builder.HasOne(e => e.Study).WithMany(e => e.EdocCategories).OnDelete(DeleteBehavior.Restrict);
    }
}