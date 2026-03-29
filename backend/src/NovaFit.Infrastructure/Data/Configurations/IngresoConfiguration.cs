using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NovaFit.Domain.Entities;

namespace NovaFit.Infrastructure.Data.Configurations;

public class IngresoConfiguration : IEntityTypeConfiguration<Ingreso>
{
    public void Configure(EntityTypeBuilder<Ingreso> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.ClienteId)
            .IsRequired();

        builder.Property(i => i.MembresiaId)
            .IsRequired(false);

        builder.Property(i => i.FechaHoraIngreso)
            .IsRequired();

        builder.Property(i => i.Permitido)
            .IsRequired();

        builder.Property(i => i.MotivoAlerta)
            .HasMaxLength(500);

        builder.Property(i => i.CreadoEn)
            .IsRequired();

        builder.HasIndex(i => new { i.ClienteId, i.FechaHoraIngreso });
    }
}
