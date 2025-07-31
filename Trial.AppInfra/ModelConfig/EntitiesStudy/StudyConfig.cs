using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trial.Domain.EntitiesStudy;

namespace Trial.AppInfra.ModelConfig.EntitiesStudy;

public class StudyConfig : IEntityTypeConfiguration<Study>
{
    public void Configure(EntityTypeBuilder<Study> builder)
    {
        builder.HasIndex(e => e.StudyId);
        builder.Property(x => x.StudyId).HasDefaultValueSql("NEWSEQUENTIALID()");
        builder.HasIndex(e => new { e.StudyNumber, e.CorporationId }).IsUnique();
        builder.Property(e => e.Protocol).UseCollation("Latin1_General_CI_AS"); //Para poderlo volver Collation CI
        //Proteccion de Borrado en Cascada
        builder.HasOne(e => e.TherapeuticArea).WithMany(e => e.Studies).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(e => e.Enrolling).WithMany(e => e.Studies).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(e => e.Indication).WithMany(e => e.Studies).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(e => e.Sponsor).WithMany(e => e.Studies).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(e => e.Usuario).WithMany(e => e.Studies).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(e => e.Irb).WithMany(e => e.Studies).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(e => e.Cro).WithMany(e => e.Studies).OnDelete(DeleteBehavior.Restrict);
    }
}