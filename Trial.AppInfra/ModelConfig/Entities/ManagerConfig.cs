using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trial.Domain.Entities;

namespace Trial.AppInfra.ModelConfig.Entities;

public class ManagerConfig : IEntityTypeConfiguration<Manager>
{
    public void Configure(EntityTypeBuilder<Manager> builder)
    {
        builder.HasKey(e => e.ManagerId);
        builder.HasIndex(e => e.UserName).IsUnique();
        builder.HasIndex(x => new { x.FullName, x.NroDocument }).IsUnique();
        //Evitar el borrado en cascada
        builder.HasOne(e => e.Corporation).WithMany(c => c.Managers).OnDelete(DeleteBehavior.Restrict);
    }
}