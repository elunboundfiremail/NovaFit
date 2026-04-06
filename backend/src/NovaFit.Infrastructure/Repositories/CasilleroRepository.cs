using Microsoft.EntityFrameworkCore;
using NovaFit.Application.Interfaces;
using NovaFit.Domain.Entities;
using NovaFit.Infrastructure.Data;
using System.Text.RegularExpressions;

namespace NovaFit.Infrastructure.Repositories;

public class CasilleroRepository : ICasilleroRepository
{
    private readonly NovaFitDbContext _context;

    public CasilleroRepository(NovaFitDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Casillero>> ObtenerTodos()
    {
        return await _context.Casilleros
            .Where(c => !c.Eliminado)
            .OrderBy(c => c.Numero)
            .ToListAsync();
    }

    public async Task<IEnumerable<Casillero>> ObtenerDisponibles()
    {
        return await _context.Casilleros
            .Where(c => c.Estado == "DISPONIBLE" && !c.Eliminado)
            .OrderBy(c => c.Numero)
            .ToListAsync();
    }

    public async Task<Casillero?> ObtenerPorId(Guid id)
    {
        return await _context.Casilleros
            .FirstOrDefaultAsync(c => c.Id == id && !c.Eliminado);
    }

    public async Task<Casillero?> ObtenerPorNumero(int numero)
    {
        return await _context.Casilleros
            .FirstOrDefaultAsync(c => c.Numero == numero && !c.Eliminado);
    }

    public async Task<Casillero> CrearCasillero(Casillero casillero)
    {
        _context.Casilleros.Add(casillero);
        await _context.SaveChangesAsync();
        return casillero;
    }

    public async Task<Casillero> ActualizarCasillero(Casillero casillero)
    {
        _context.Casilleros.Update(casillero);
        await _context.SaveChangesAsync();
        return casillero;
    }

    public async Task<PrestamoCasillero> CrearPrestamo(PrestamoCasillero prestamo)
    {
        _context.PrestamosCasilleros.Add(prestamo);
        await _context.SaveChangesAsync();
        return prestamo;
    }

    public async Task<PrestamoCasillero?> ObtenerPrestamoPorId(Guid id)
    {
        return await _context.PrestamosCasilleros
            .Include(p => p.Casillero)
            .Include(p => p.Ingreso)
                .ThenInclude(i => i.Cliente)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<PrestamoCasillero> ActualizarPrestamo(PrestamoCasillero prestamo)
    {
        _context.PrestamosCasilleros.Update(prestamo);
        await _context.SaveChangesAsync();
        return prestamo;
    }

    public async Task<IEnumerable<PrestamoCasillero>> ObtenerPrestamosActivos()
    {
        return await _context.PrestamosCasilleros
            .Include(p => p.Casillero)
            .Include(p => p.Ingreso)
                .ThenInclude(i => i.Cliente)
            .Where(p => p.FechaDevolucion == null)
            .OrderByDescending(p => p.FechaPrestamo)
            .ToListAsync();
    }

    public async Task<IEnumerable<PrestamoCasillero>> ObtenerHistorialPorCasillero(Guid casilleroId)
    {
        return await _context.PrestamosCasilleros
            .Include(p => p.Casillero)
            .Include(p => p.Ingreso)
                .ThenInclude(i => i.Cliente)
            .Where(p => p.CasilleroId == casilleroId)
            .OrderByDescending(p => p.FechaPrestamo)
            .ToListAsync();
    }

    public async Task<bool> TienePrestamoActivo(Guid casilleroId)
    {
        return await _context.PrestamosCasilleros
            .AnyAsync(p => p.CasilleroId == casilleroId && p.FechaDevolucion == null);
    }

    public async Task<bool> TienePrestamoActivoPorIngreso(Guid ingresoId)
    {
        return await _context.PrestamosCasilleros
            .AnyAsync(p => p.IngresoId == ingresoId && p.FechaDevolucion == null && !p.Eliminado);
    }

    public async Task<int> ObtenerSiguienteNumeroTicket()
    {
        var hoy = DateTime.UtcNow.AddHours(-4).Date;
        var manana = hoy.AddDays(1);

        var cantidadHoy = await _context.PrestamosCasilleros
            .Where(p => !p.Eliminado
                && p.NumeroTicket != null
                && p.FechaPrestamo >= hoy
                && p.FechaPrestamo < manana)
            .CountAsync();

        return cantidadHoy + 1;
    }

    public async Task<int> ObtenerSiguienteNumeroLlave()
    {
        var llaves = await _context.PrestamosCasilleros
            .Include(p => p.Casillero)
            .Where(p => !p.Eliminado && p.NumeroLlave != null && p.Casillero != null && p.Casillero.Tipo != "ESTANTE_RECEPCION")
            .Select(p => p.NumeroLlave!)
            .ToListAsync();

        return ObtenerSiguienteConsecutivo(llaves);
    }

    private static int ObtenerSiguienteConsecutivo(IEnumerable<string> valores)
    {
        var maximo = 0;

        foreach (var valor in valores)
        {
            if (string.IsNullOrWhiteSpace(valor))
                continue;

            var coincidencia = Regex.Match(valor, @"(\d+)$");
            if (!coincidencia.Success)
                continue;

            if (int.TryParse(coincidencia.Groups[1].Value, out var numero) && numero > maximo)
                maximo = numero;
        }

        return maximo + 1;
    }
}

