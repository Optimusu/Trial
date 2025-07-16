﻿using Microsoft.EntityFrameworkCore;
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
        builder.Property(e => e.FullName).UseCollation("Latin1_General_CI_AS"); //Para poderlo volver Collation CI
        //Evitar el borrado en cascada
        builder.HasOne(e => e.Corporation).WithMany(c => c.Managers).OnDelete(DeleteBehavior.Restrict);
    }
}