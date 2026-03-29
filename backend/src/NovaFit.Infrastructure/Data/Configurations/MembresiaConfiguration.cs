using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NovaFit.Domain.Entities;

namespace NovaFit.Infrastructure.Data.Configurations;

public class MembresiaConfiguration : IEntityTypeConfiguration<Membresia>
{
    public void Configure(EntityTypeBuilder<Membresia> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.ClienteId)
            .IsRequired();

        builder.Property(m => m.TipoPlan)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(m => m.FechaInicio)
            .IsRequired();

        builder.Property(m => m.FechaFin)
            .IsRequired();

        builder.Property(m => m.Costo)
            .IsRequired()
            .HasColumnType("decimal(10,2)");

        builder.Property(m => m.Observacion)
            .HasMaxLength(500);

        builder.Property(m => m.Estado)
            .IsRequired()
            .HasMaxLength(20)
            .HasDefaultValue("activa");

        builder.Property(m => m.CreadoEn)
            .IsRequired();

        // Relaciones
        builder.HasMany(m => m.Ingresos)
            .WithOne()
            .HasForeignKey(i => i.MembresiaId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
