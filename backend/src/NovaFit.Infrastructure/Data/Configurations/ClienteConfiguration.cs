using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NovaFit.Domain.Entities;

namespace NovaFit.Infrastructure.Data.Configurations;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Ci)
            .IsRequired();

        builder.HasIndex(c => c.Ci)
            .IsUnique();

        builder.Property(c => c.Nombres)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.ApellidoPaterno)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.ApellidoMaterno)
            .HasMaxLength(100);

        builder.Property(c => c.TipoCliente)
            .IsRequired()
            .HasMaxLength(20)
            .HasDefaultValue("nuevo");

        builder.Property(c => c.FechaRegistro)
            .IsRequired();

        builder.Property(c => c.Activo)
            .IsRequired()
            .HasDefaultValue(true);

        // Relaciones
        builder.HasMany(c => c.Membresias)
            .WithOne()
            .HasForeignKey(m => m.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.Ingresos)
            .WithOne()
            .HasForeignKey(i => i.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.PrestamosCasilleros)
            .WithOne()
            .HasForeignKey(p => p.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
