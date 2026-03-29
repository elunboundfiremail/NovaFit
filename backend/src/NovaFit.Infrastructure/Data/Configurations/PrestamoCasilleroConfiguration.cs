using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NovaFit.Domain.Entities;

namespace NovaFit.Infrastructure.Data.Configurations;

public class PrestamoCasilleroConfiguration : IEntityTypeConfiguration<PrestamoCasillero>
{
    public void Configure(EntityTypeBuilder<PrestamoCasillero> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.CasilleroId)
            .IsRequired();

        builder.Property(p => p.ClienteId)
            .IsRequired();

        builder.Property(p => p.FechaHoraPrestamo)
            .IsRequired();

        builder.Property(p => p.FechaHoraDevolucion)
            .IsRequired(false);

        builder.HasIndex(p => new { p.CasilleroId, p.FechaHoraPrestamo });
    }
}
