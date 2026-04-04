using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NovaFit.Domain.Entities;

namespace NovaFit.Infrastructure.Data.Configurations;

public class CasilleroConfiguration : IEntityTypeConfiguration<Casillero>
{
    public void Configure(EntityTypeBuilder<Casillero> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Numero)
            .IsRequired();

        builder.HasIndex(c => c.Numero)
            .IsUnique();

        builder.Property(c => c.Eliminado)
            .IsRequired()
            .HasDefaultValue(true);

        // Relaciones
        builder.HasMany(c => c.Prestamos)
            .WithOne(p => p.Casillero)
            .HasForeignKey(p => p.CasilleroId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

