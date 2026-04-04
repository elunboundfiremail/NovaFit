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

        builder.Property(i => i.SuscripcionId)
            .IsRequired(false);

        builder.Property(i => i.FechaIngreso)
            .IsRequired();

        builder.Property(i => i.HoraIngreso)
            .IsRequired();

        builder.Property(i => i.HoraSalida)
            .IsRequired(false);

        builder.Property(i => i.SalidaRegistrada)
            .IsRequired();

        builder.Property(i => i.FechaCreacion)
            .IsRequired();

        builder.HasIndex(i => new { i.ClienteId, i.FechaIngreso });

        // Soft delete query filter
        builder.HasQueryFilter(i => !i.Eliminado);
    }
}
