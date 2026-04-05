using Microsoft.EntityFrameworkCore;
using NovaFit.Application.Interfaces;
using NovaFit.Domain.Entities;
using NovaFit.Infrastructure.Data;

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
        return await _context.Casilleros.FindAsync(id);
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
            .Where(p => p.FechaDevolucion == null)
            .OrderByDescending(p => p.FechaPrestamo)
            .ToListAsync();
    }

    public async Task<IEnumerable<PrestamoCasillero>> ObtenerHistorialPorCasillero(Guid casilleroId)
    {
        return await _context.PrestamosCasilleros
            .Include(p => p.Casillero)
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
}

