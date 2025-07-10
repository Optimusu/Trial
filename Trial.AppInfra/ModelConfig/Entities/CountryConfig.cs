using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trial.Domain.Entities;

namespace Trial.AppInfra.ModelConfig.Entities;

public class CountryConfig : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.HasKey(e => e.CountryId);
        builder.HasIndex(e => e.Name).IsUnique();
    }
}