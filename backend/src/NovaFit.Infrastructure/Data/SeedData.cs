using Microsoft.EntityFrameworkCore;
using NovaFit.Domain.Entities;

namespace NovaFit.Infrastructure.Data;

public static class SeedData
{
    public static async Task Seed(NovaFitDbContext context)
    {
        var ahora = DateTime.UtcNow.AddHours(-4);

        if (!await context.Clientes.AnyAsync())
        {
            var clientes = new[]
            {
                new Cliente
                {
                    Id = Guid.NewGuid(),
                    Ci = 12345678,
                    Nombre = "Juan",
                    Apellido = "Pérez",
                    Email = "juan@email.com",
                    Telefono = "75123456",
                    FechaRegistro = ahora.AddDays(-30)
                },
                new Cliente
                {
                    Id = Guid.NewGuid(),
                    Ci = 87654321,
                    Nombre = "María",
                    Apellido = "González",
                    Email = "maria@email.com",
                    Telefono = "75654321",
                    FechaRegistro = ahora.AddDays(-20)
                }
            };

            await context.Clientes.AddRangeAsync(clientes);
            await context.SaveChangesAsync();
        }

        if (!await context.Casilleros.AnyAsync())
        {
            var casilleros = Enumerable.Range(1, 12)
                .Select(numero => new Casillero
                {
                    Id = Guid.NewGuid(),
                    Numero = numero,
                    Tipo = numero <= 4 ? "FIJO" : numero <= 8 ? "TEMPORAL" : "ESTANTE_RECEPCION",
                    Estado = numero == 12 ? "MANTENIMIENTO" : "DISPONIBLE",
                    Ubicacion = numero <= 6 ? "Planta Baja" : "Planta Alta",
                    FechaCreacion = ahora
                });

            await context.Casilleros.AddRangeAsync(casilleros);
            await context.SaveChangesAsync();
        }
    }
}
