using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NovaFit.Domain.Entities;

namespace NovaFit.Infrastructure.Data.Configurations;

public class PromocionFestivaConfiguration : IEntityTypeConfiguration<PromocionFestiva>
{
    public void Configure(EntityTypeBuilder<PromocionFestiva> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nombre)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Descripcion)
            .HasMaxLength(1000);

        builder.Property(p => p.FechaInicio)
            .IsRequired();

        builder.Property(p => p.FechaFin)
            .IsRequired();

        builder.Property(p => p.PorcentajeDescuento)
            .IsRequired()
            .HasColumnType("decimal(5,2)");

        builder.Property(p => p.Activa)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(p => p.CreadoEn)
            .IsRequired();

        builder.HasIndex(p => new { p.FechaInicio, p.FechaFin });
    }
}
