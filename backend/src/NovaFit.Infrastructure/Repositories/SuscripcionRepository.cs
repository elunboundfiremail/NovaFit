using Microsoft.EntityFrameworkCore;
using NovaFit.Application.Interfaces;
using NovaFit.Domain.Entities;
using NovaFit.Infrastructure.Data;

namespace NovaFit.Infrastructure.Repositories;

public class SuscripcionRepository : ISuscripcionRepository
{
    private readonly NovaFitDbContext _context;

    public SuscripcionRepository(NovaFitDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Suscripcion>> ObtenerTodas()
    {
        return await _context.Suscripcions
            .Include(m => m.Cliente)
            .OrderByDescending(m => m.FechaInicio)
            .ToListAsync();
    }

    public async Task<Suscripcion?> ObtenerPorId(Guid id)
    {
        return await _context.Suscripcions
            .Include(m => m.Cliente)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<Suscripcion?> ObtenerActivaPorCliente(Guid clienteId)
    {
        var ahora = DateTime.UtcNow.AddHours(-4);

        return await _context.Suscripcions
            .Where(m => m.ClienteId == clienteId
                && m.Estado == "activa"
                && m.FechaVencimiento >= ahora)
            .OrderByDescending(m => m.FechaVencimiento)
            .FirstOrDefaultAsync();
    }

    public async Task<Suscripcion?> ObtenerUltimaPorCliente(Guid clienteId)
    {
        return await _context.Suscripcions
            .Where(m => m.ClienteId == clienteId)
            .OrderByDescending(m => m.FechaVencimiento)
            .FirstOrDefaultAsync();
    }

    public async Task<Suscripcion> Crear(Suscripcion Suscripcion)
    {
        _context.Suscripcions.Add(Suscripcion);
        await _context.SaveChangesAsync();
        return Suscripcion;
    }

    public async Task<Suscripcion> Actualizar(Suscripcion Suscripcion)
    {
        _context.Suscripcions.Update(Suscripcion);
        await _context.SaveChangesAsync();
        return Suscripcion;
    }
}
