using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trial.Domain.Entities;

namespace Trial.AppInfra.ModelConfig.Entities;

public class SoftPlanConfig : IEntityTypeConfiguration<SoftPlan>
{
    public void Configure(EntityTypeBuilder<SoftPlan> builder)
    {
        builder.HasKey(e => e.SoftPlanId);
        builder.HasIndex(x => x.Name).IsUnique();
        builder.Property(e => e.Price).HasPrecision(18, 2);
    }
}