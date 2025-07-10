using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trial.Domain.EntitesSoftSec;

namespace Trial.AppInfra.ModelConfig.EntitesSoftSec;

public class UsuarioConfig : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.HasKey(e => e.UsuarioId);
        builder.HasIndex(e => e.UserName).IsUnique();
        builder.HasIndex(x => new { x.FullName, x.Nro_Document, x.CorporationId }).IsUnique();
        //Evitar el borrado en cascada
        builder.HasOne(e => e.Corporation).WithMany(c => c.Usuarios).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(e => e.DocumentType).WithMany(c => c.Usuarios).OnDelete(DeleteBehavior.Restrict);
    }
}