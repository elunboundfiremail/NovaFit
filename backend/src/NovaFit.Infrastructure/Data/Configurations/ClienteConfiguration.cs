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

        builder.Property(c => c.Nombre)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Apellido)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Email)
            .HasMaxLength(150);

        builder.Property(c => c.Telefono)
            .HasMaxLength(20);

        builder.Property(c => c.FechaRegistro)
            .IsRequired();

        // Relaciones
        builder.HasMany(c => c.Suscripciones)
            .WithOne(m => m.Cliente)
            .HasForeignKey(m => m.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.Ingresos)
            .WithOne(i => i.Cliente)
            .HasForeignKey(i => i.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);

        // Soft delete query filter
        builder.HasQueryFilter(c => !c.Eliminado);
    }
}
