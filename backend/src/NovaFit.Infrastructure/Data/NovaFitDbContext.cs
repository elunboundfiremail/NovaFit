using Microsoft.EntityFrameworkCore;
using NovaFit.Domain.Entities;

namespace NovaFit.Infrastructure.Data;

public class NovaFitDbContext : DbContext
{
    public NovaFitDbContext(DbContextOptions<NovaFitDbContext> options)
        : base(options)
    {
    }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Suscripcion> Suscripcions { get; set; }
    public DbSet<Ingreso> Ingresos { get; set; }
    public DbSet<Casillero> Casilleros { get; set; }
    public DbSet<PrestamoCasillero> PrestamosCasilleros { get; set; }
    public DbSet<PromocionFestiva> PromocionesFestivas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Aplicar todas las configuraciones de la carpeta Configurations
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NovaFitDbContext).Assembly);

        // Configurar Nombre de tablas en minusculas (convencion PostgreSQL)
        modelBuilder.Entity<Cliente>().ToTable("clientes");
        modelBuilder.Entity<Suscripcion>().ToTable("Suscripcions");
        modelBuilder.Entity<Ingreso>().ToTable("ingresos");
        modelBuilder.Entity<Casillero>().ToTable("casilleros");
        modelBuilder.Entity<PrestamoCasillero>().ToTable("prestamos_casilleros");
        modelBuilder.Entity<PromocionFestiva>().ToTable("promociones_festivas");
    }
}
