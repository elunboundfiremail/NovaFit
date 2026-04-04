using Microsoft.EntityFrameworkCore;
using NovaFit.Domain.Entities;

namespace NovaFit.Infrastructure.Data;

public static class SeedData
{
    public static async Task Seed(NovaFitDbContext context)
    {
        if (await context.Clientes.AnyAsync())
            return; // Ya hay datos

        var ahora = DateTime.UtcNow.AddHours(-4);

        // Crear clientes básicos
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
}
