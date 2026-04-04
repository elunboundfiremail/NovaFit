using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NovaFit.Domain.Entities;

namespace NovaFit.Infrastructure.Data.Configurations;

public class SuscripcionConfiguration : IEntityTypeConfiguration<Suscripcion>
{
    public void Configure(EntityTypeBuilder<Suscripcion> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Tipo)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(s => s.Precio)
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        builder.Property(s => s.Estado)
            .IsRequired()
            .HasMaxLength(20)
            .HasDefaultValue("ACTIVA");

        builder.Property(s => s.DescuentoAplicado)
            .HasColumnType("decimal(5,2)")
            .HasDefaultValue(0.00m);

        builder.Property(s => s.IngresosTotalesUsados)
            .HasDefaultValue(0);

        builder.Property(s => s.Eliminado)
            .HasDefaultValue(false);

        // Relaciones
        builder.HasOne(s => s.Cliente)
            .WithMany(c => c.Suscripciones)
            .HasForeignKey(s => s.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.CasilleroFijo)
            .WithMany(c => c.SuscripcionesConCasilleroFijo)
            .HasForeignKey(s => s.CasilleroFijoId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(s => s.Promocion)
            .WithMany(p => p.Suscripciones)
            .HasForeignKey(s => s.PromocionId)
            .OnDelete(DeleteBehavior.SetNull);

        // Índices
        builder.HasIndex(s => s.ClienteId);
        builder.HasIndex(s => s.Estado);
        builder.HasIndex(s => s.Tipo);
        builder.HasIndex(s => new { s.ClienteId, s.Estado });
        
        // Query filter para soft delete
        builder.HasQueryFilter(s => !s.Eliminado);
    }
}

