using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NovaFit.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NovaFit.Infrastructure.Data;

/// <summary>
/// Clase para inicializar la base de datos con datos de prueba realistas para Bolivia
/// Se ejecuta automaticamente al iniciar la aplicacion si la BD esta vacia
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Inicializa la base de datos con datos de prueba
    /// Solo inserta datos si las tablas estan vacias
    /// </summary>
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<NovaFitDbContext>();

        // Aplicar migraciones pendientes automaticamente
        await context.Database.MigrateAsync();

        // Solo insertar datos si la tabla Clientes esta vacia
        if (context.Clientes.Any())
        {
            return; // Ya hay datos, no hacer nada
        }

        // FECHAS DE REFERENCIA (Bolivia UTC-4)
        var ahora = DateTime.UtcNow.AddHours(-4);
        var hace30Dias = ahora.AddDays(-30);
        var hace15Dias = ahora.AddDays(-15);
        var hace7Dias = ahora.AddDays(-7);

        // ========================================
        // 1. CLIENTES (10 clientes de ejemplo)
        // ========================================
        var clientes = new[]
        {
            new Cliente
            {
                Id = Guid.NewGuid(),
                Ci = 7654321,
                Nombres = "Juan Carlos",
                ApellidoPaterno = "Perez",
                ApellidoMaterno = "Mamani",
                TipoCliente = "nuevo",
                FechaRegistro = hace30Dias,
                Activo = true
            },
            new Cliente
            {
                Id = Guid.NewGuid(),
                Ci = 8765432,
                Nombres = "Maria Elena",
                ApellidoPaterno = "Rodriguez",
                ApellidoMaterno = "Quispe",
                TipoCliente = "nuevo",
                FechaRegistro = hace30Dias,
                Activo = true
            },
            new Cliente
            {
                Id = Guid.NewGuid(),
                Ci = 9876543,
                Nombres = "Carlos Alberto",
                ApellidoPaterno = "Gutierrez",
                ApellidoMaterno = "Condori",
                TipoCliente = "nuevo",
                FechaRegistro = hace15Dias,
                Activo = true
            },
            new Cliente
            {
                Id = Guid.NewGuid(),
                Ci = 6543210,
                Nombres = "Ana Patricia",
                ApellidoPaterno = "Flores",
                ApellidoMaterno = "Mamani",
                TipoCliente = "nuevo",
                FechaRegistro = hace15Dias,
                Activo = true
            },
            new Cliente
            {
                Id = Guid.NewGuid(),
                Ci = 5432109,
                Nombres = "Luis Fernando",
                ApellidoPaterno = "Choque",
                ApellidoMaterno = "Apaza",
                TipoCliente = "nuevo",
                FechaRegistro = hace7Dias,
                Activo = true
            },
            new Cliente
            {
                Id = Guid.NewGuid(),
                Ci = 4321098,
                Nombres = "Sandra Viviana",
                ApellidoPaterno = "Ticona",
                ApellidoMaterno = "Huanca",
                TipoCliente = "nuevo",
                FechaRegistro = hace7Dias,
                Activo = true
            },
            new Cliente
            {
                Id = Guid.NewGuid(),
                Ci = 3210987,
                Nombres = "Roberto Miguel",
                ApellidoPaterno = "Vargas",
                ApellidoMaterno = "Limachi",
                TipoCliente = "nuevo",
                FechaRegistro = hace7Dias,
                Activo = true
            },
            new Cliente
            {
                Id = Guid.NewGuid(),
                Ci = 2109876,
                Nombres = "Paola Andrea",
                ApellidoPaterno = "Mamani",
                ApellidoMaterno = "Quispe",
                TipoCliente = "nuevo",
                FechaRegistro = hace7Dias,
                Activo = true
            },
            new Cliente
            {
                Id = Guid.NewGuid(),
                Ci = 1098765,
                Nombres = "Diego Alejandro",
                ApellidoPaterno = "Condori",
                ApellidoMaterno = "Poma",
                TipoCliente = "nuevo",
                FechaRegistro = ahora.AddDays(-3),
                Activo = true
            },
            new Cliente
            {
                Id = Guid.NewGuid(),
                Ci = 9087654,
                Nombres = "Fernanda Isabel",
                ApellidoPaterno = "Apaza",
                ApellidoMaterno = "Nina",
                TipoCliente = "nuevo",
                FechaRegistro = ahora.AddDays(-1),
                Activo = true
            }
        };

        await context.Clientes.AddRangeAsync(clientes);
        await context.SaveChangesAsync();

        // ========================================
        // 2. MEMBRESIAS (8 activas + 2 vencidas)
        // ========================================
        var membresias = new[]
        {
            // Membresias ANUALES activas
            new Membresia
            {
                Id = Guid.NewGuid(),
                ClienteId = clientes[0].Id,
                TipoPlan = "anual",
                FechaInicio = hace30Dias,
                FechaFin = hace30Dias.AddDays(365),
                Costo = 1200.00m,
                Estado = "activa",
                CreadoEn = hace30Dias
            },
            new Membresia
            {
                Id = Guid.NewGuid(),
                ClienteId = clientes[1].Id,
                TipoPlan = "anual",
                FechaInicio = hace30Dias,
                FechaFin = hace30Dias.AddDays(365),
                Costo = 1200.00m,
                Estado = "activa",
                CreadoEn = hace30Dias
            },
            // Membresias MENSUALES activas
            new Membresia
            {
                Id = Guid.NewGuid(),
                ClienteId = clientes[2].Id,
                TipoPlan = "mensual",
                FechaInicio = hace15Dias,
                FechaFin = hace15Dias.AddDays(30),
                Costo = 150.00m,
                Estado = "activa",
                CreadoEn = hace15Dias
            },
            new Membresia
            {
                Id = Guid.NewGuid(),
                ClienteId = clientes[3].Id,
                TipoPlan = "mensual",
                FechaInicio = hace15Dias,
                FechaFin = hace15Dias.AddDays(30),
                Costo = 150.00m,
                Estado = "activa",
                CreadoEn = hace15Dias
            },
            new Membresia
            {
                Id = Guid.NewGuid(),
                ClienteId = clientes[4].Id,
                TipoPlan = "mensual",
                FechaInicio = hace7Dias,
                FechaFin = hace7Dias.AddDays(30),
                Costo = 150.00m,
                Estado = "activa",
                CreadoEn = hace7Dias
            },
            new Membresia
            {
                Id = Guid.NewGuid(),
                ClienteId = clientes[5].Id,
                TipoPlan = "mensual",
                FechaInicio = hace7Dias,
                FechaFin = hace7Dias.AddDays(30),
                Costo = 150.00m,
                Estado = "activa",
                CreadoEn = hace7Dias
            },
            new Membresia
            {
                Id = Guid.NewGuid(),
                ClienteId = clientes[6].Id,
                TipoPlan = "mensual",
                FechaInicio = hace7Dias,
                FechaFin = hace7Dias.AddDays(30),
                Costo = 150.00m,
                Estado = "activa",
                CreadoEn = hace7Dias
            },
            new Membresia
            {
                Id = Guid.NewGuid(),
                ClienteId = clientes[7].Id,
                TipoPlan = "mensual",
                FechaInicio = hace7Dias,
                FechaFin = hace7Dias.AddDays(30),
                Costo = 150.00m,
                Estado = "activa",
                CreadoEn = hace7Dias
            },
            // Membresias VENCIDAS (para probar validaciones)
            new Membresia
            {
                Id = Guid.NewGuid(),
                ClienteId = clientes[8].Id,
                TipoPlan = "mensual",
                FechaInicio = ahora.AddDays(-45),
                FechaFin = ahora.AddDays(-15),
                Costo = 150.00m,
                Estado = "vencida",
                CreadoEn = ahora.AddDays(-45)
            },
            new Membresia
            {
                Id = Guid.NewGuid(),
                ClienteId = clientes[9].Id,
                TipoPlan = "mensual",
                FechaInicio = ahora.AddDays(-60),
                FechaFin = ahora.AddDays(-30),
                Costo = 150.00m,
                Estado = "vencida",
                CreadoEn = ahora.AddDays(-60)
            }
        };

        await context.Membresias.AddRangeAsync(membresias);
        await context.SaveChangesAsync();

        // ========================================
        // 3. INGRESOS (15 registros de accesos)
        // ========================================
        var ingresos = new[]
        {
            // Ingresos exitosos - ultimos 7 dias
            new Ingreso
            {
                Id = Guid.NewGuid(),
                ClienteId = clientes[0].Id,
                MembresiaId = membresias[0].Id,
                FechaHoraIngreso = hace7Dias.AddHours(8).AddMinutes(15),
                Permitido = true,
                CreadoEn = hace7Dias.AddHours(8).AddMinutes(15)
            },
            new Ingreso
            {
                Id = Guid.NewGuid(),
                ClienteId = clientes[1].Id,
                MembresiaId = membresias[1].Id,
                FechaHoraIngreso = hace7Dias.AddHours(7).AddMinutes(45),
                Permitido = true,
                CreadoEn = hace7Dias.AddHours(7).AddMinutes(45)
            },
            new Ingreso
            {
                Id = Guid.NewGuid(),
                ClienteId = clientes[2].Id,
                MembresiaId = membresias[2].Id,
                FechaHoraIngreso = hace7Dias.AddHours(9).AddMinutes(20),
                Permitido = true,
                CreadoEn = hace7Dias.AddHours(9).AddMinutes(20)
            },
            new Ingreso
            {
                Id = Guid.NewGuid(),
                ClienteId = clientes[3].Id,
                MembresiaId = membresias[3].Id,
                FechaHoraIngreso = hace7Dias.AddHours(6).AddMinutes(30),
                Permitido = true,
                CreadoEn = hace7Dias.AddHours(6).AddMinutes(30)
            },
            new Ingreso
            {
                Id = Guid.NewGuid(),
                ClienteId = clientes[4].Id,
                MembresiaId = membresias[4].Id,
                FechaHoraIngreso = hace7Dias.AddHours(20).AddMinutes(15),
                Permitido = true,
                CreadoEn = hace7Dias.AddHours(20).AddMinutes(15)
            },
            new Ingreso
            {
                Id = Guid.NewGuid(),
                ClienteId = clientes[0].Id,
                MembresiaId = membresias[0].Id,
                FechaHoraIngreso = hace7Dias.AddDays(1).AddHours(18).AddMinutes(30),
                Permitido = true,
                CreadoEn = hace7Dias.AddDays(1).AddHours(18).AddMinutes(30)
            },
            new Ingreso
            {
                Id = Guid.NewGuid(),
                ClienteId = clientes[1].Id,
                MembresiaId = membresias[1].Id,
                FechaHoraIngreso = hace7Dias.AddDays(2).AddHours(19).AddMinutes(10),
                Permitido = true,
                CreadoEn = hace7Dias.AddDays(2).AddHours(19).AddMinutes(10)
            },
            new Ingreso
            {
                Id = Guid.NewGuid(),
                ClienteId = clientes[2].Id,
                MembresiaId = membresias[2].Id,
                FechaHoraIngreso = hace7Dias.AddDays(3).AddHours(17).AddMinutes(45),
                Permitido = true,
                CreadoEn = hace7Dias.AddDays(3).AddHours(17).AddMinutes(45)
            },
            // INGRESOS DENEGADOS - Membresia vencida
            new Ingreso
            {
                Id = Guid.NewGuid(),
                ClienteId = clientes[8].Id,
                MembresiaId = null,
                FechaHoraIngreso = ahora.AddDays(-10).AddHours(10).AddMinutes(30),
                Permitido = false,
                CreadoEn = ahora.AddDays(-10).AddHours(10).AddMinutes(30)
            },
            new Ingreso
            {
                Id = Guid.NewGuid(),
                ClienteId = clientes[9].Id,
                MembresiaId = null,
                FechaHoraIngreso = ahora.AddDays(-25).AddHours(14).AddMinutes(20),
                Permitido = false,
                CreadoEn = ahora.AddDays(-25).AddHours(14).AddMinutes(20)
            },
            // Ingresos recientes (ultimas 24 horas)
            new Ingreso
            {
                Id = Guid.NewGuid(),
                ClienteId = clientes[0].Id,
                MembresiaId = membresias[0].Id,
                FechaHoraIngreso = ahora.AddDays(-1).AddHours(8).AddMinutes(5),
                Permitido = true,
                CreadoEn = ahora.AddDays(-1).AddHours(8).AddMinutes(5)
            },
            new Ingreso
            {
                Id = Guid.NewGuid(),
                ClienteId = clientes[1].Id,
                MembresiaId = membresias[1].Id,
                FechaHoraIngreso = ahora.AddDays(-1).AddHours(18).AddMinutes(50),
                Permitido = true,
                CreadoEn = ahora.AddDays(-1).AddHours(18).AddMinutes(50)
            },
            new Ingreso
            {
                Id = Guid.NewGuid(),
                ClienteId = clientes[2].Id,
                MembresiaId = membresias[2].Id,
                FechaHoraIngreso = ahora.AddHours(-12).AddMinutes(30),
                Permitido = true,
                CreadoEn = ahora.AddHours(-12).AddMinutes(30)
            },
            new Ingreso
            {
                Id = Guid.NewGuid(),
                ClienteId = clientes[3].Id,
                MembresiaId = membresias[3].Id,
                FechaHoraIngreso = ahora.AddHours(-10).AddMinutes(15),
                Permitido = true,
                CreadoEn = ahora.AddHours(-10).AddMinutes(15)
            },
            new Ingreso
            {
                Id = Guid.NewGuid(),
                ClienteId = clientes[4].Id,
                MembresiaId = membresias[4].Id,
                FechaHoraIngreso = ahora.AddHours(-8).AddMinutes(45),
                Permitido = true,
                CreadoEn = ahora.AddHours(-8).AddMinutes(45)
            }
        };

        await context.Ingresos.AddRangeAsync(ingresos);
        await context.SaveChangesAsync();

        // ========================================
        // 4. CASILLEROS (20 casilleros)
        // ========================================
        var casilleros = new Casillero[20];
        for (int i = 0; i < 20; i++)
        {
            casilleros[i] = new Casillero
            {
                Id = Guid.NewGuid(),
                Numero = i + 1,
                Disponible = i >= 8, // 1-8 ocupados, 9-20 disponibles
                Activo = true,
                CreadoEn = hace30Dias
            };
        }

        await context.Casilleros.AddRangeAsync(casilleros);
        await context.SaveChangesAsync();

        // ========================================
        // 5. PRESTAMOS CASILLEROS (8 prestamos activos)
        // ========================================
        var prestamos = new[]
        {
            new PrestamoCasillero
            {
                Id = Guid.NewGuid(),
                CasilleroId = casilleros[0].Id,
                ClienteId = clientes[0].Id,
                DocumentoRetenido = "CI",
                FechaHoraPrestamo = hace7Dias.AddHours(8).AddMinutes(20),
                CreadoEn = hace7Dias.AddHours(8).AddMinutes(20)
            },
            new PrestamoCasillero
            {
                Id = Guid.NewGuid(),
                CasilleroId = casilleros[1].Id,
                ClienteId = clientes[1].Id,
                DocumentoRetenido = "CI",
                FechaHoraPrestamo = hace7Dias.AddHours(7).AddMinutes(50),
                CreadoEn = hace7Dias.AddHours(7).AddMinutes(50)
            },
            new PrestamoCasillero
            {
                Id = Guid.NewGuid(),
                CasilleroId = casilleros[2].Id,
                ClienteId = clientes[2].Id,
                DocumentoRetenido = "CI",
                FechaHoraPrestamo = hace7Dias.AddHours(9).AddMinutes(25),
                CreadoEn = hace7Dias.AddHours(9).AddMinutes(25)
            },
            new PrestamoCasillero
            {
                Id = Guid.NewGuid(),
                CasilleroId = casilleros[3].Id,
                ClienteId = clientes[3].Id,
                DocumentoRetenido = "CI",
                FechaHoraPrestamo = hace7Dias.AddHours(6).AddMinutes(35),
                CreadoEn = hace7Dias.AddHours(6).AddMinutes(35)
            },
            new PrestamoCasillero
            {
                Id = Guid.NewGuid(),
                CasilleroId = casilleros[4].Id,
                ClienteId = clientes[4].Id,
                DocumentoRetenido = "CI",
                FechaHoraPrestamo = hace7Dias.AddHours(20).AddMinutes(20),
                CreadoEn = hace7Dias.AddHours(20).AddMinutes(20)
            },
            new PrestamoCasillero
            {
                Id = Guid.NewGuid(),
                CasilleroId = casilleros[5].Id,
                ClienteId = clientes[5].Id,
                DocumentoRetenido = "CI",
                FechaHoraPrestamo = hace7Dias.AddHours(18).AddMinutes(40),
                CreadoEn = hace7Dias.AddHours(18).AddMinutes(40)
            },
            new PrestamoCasillero
            {
                Id = Guid.NewGuid(),
                CasilleroId = casilleros[6].Id,
                ClienteId = clientes[6].Id,
                DocumentoRetenido = "CI",
                FechaHoraPrestamo = hace7Dias.AddHours(17).AddMinutes(10),
                CreadoEn = hace7Dias.AddHours(17).AddMinutes(10)
            },
            new PrestamoCasillero
            {
                Id = Guid.NewGuid(),
                CasilleroId = casilleros[7].Id,
                ClienteId = clientes[7].Id,
                DocumentoRetenido = "CI",
                FechaHoraPrestamo = hace7Dias.AddHours(19).AddMinutes(30),
                CreadoEn = hace7Dias.AddHours(19).AddMinutes(30)
            }
        };

        await context.PrestamosCasilleros.AddRangeAsync(prestamos);
        await context.SaveChangesAsync();

        // ========================================
        // 6. PROMOCIONES FESTIVAS (3 promociones)
        // ========================================
        var promociones = new[]
        {
            new PromocionFestiva
            {
                Id = Guid.NewGuid(),
                Nombre = "Promocion Carnaval de Oruro 2026",
                FechaInicio = new DateTime(2026, 2, 15),
                FechaFin = new DateTime(2026, 2, 28),
                PorcentajeDescuento = 20.0m,
                Descripcion = "Descuento especial por Carnaval: 20% en membresias mensuales y anuales",
                Activa = false, // Ya paso
                CreadoEn = new DateTime(2026, 2, 1)
            },
            new PromocionFestiva
            {
                Id = Guid.NewGuid(),
                Nombre = "Aniversario NovaFit 2026",
                FechaInicio = new DateTime(2026, 4, 1),
                FechaFin = new DateTime(2026, 4, 15),
                PorcentajeDescuento = 25.0m,
                Descripcion = "Aniversario del gimnasio: 25% descuento + toalla deportiva gratis",
                Activa = true, // Activa
                CreadoEn = new DateTime(2026, 3, 1)
            },
            new PromocionFestiva
            {
                Id = Guid.NewGuid(),
                Nombre = "Semana de la Salud Bolivia 2026",
                FechaInicio = new DateTime(2026, 6, 1),
                FechaFin = new DateTime(2026, 6, 7),
                PorcentajeDescuento = 15.0m,
                Descripcion = "Semana de la Salud: 15% descuento + evaluacion fisica gratuita",
                Activa = true, // Programada
                CreadoEn = new DateTime(2026, 3, 1)
            }
        };

        await context.PromocionesFestivas.AddRangeAsync(promociones);
        await context.SaveChangesAsync();
    }
}
