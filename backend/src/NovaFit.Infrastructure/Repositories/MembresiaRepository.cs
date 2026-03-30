using Microsoft.EntityFrameworkCore;
using NovaFit.Application.Interfaces;
using NovaFit.Domain.Entities;
using NovaFit.Infrastructure.Data;

namespace NovaFit.Infrastructure.Repositories;

public class MembresiaRepository : IMembresiaRepository
{
    private readonly NovaFitDbContext _context;

    public MembresiaRepository(NovaFitDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Membresia>> ObtenerTodas()
    {
        return await _context.Membresias
            .Include(m => m.Cliente)
            .OrderByDescending(m => m.FechaInicio)
            .ToListAsync();
    }

    public async Task<Membresia?> ObtenerPorId(Guid id)
    {
        return await _context.Membresias
            .Include(m => m.Cliente)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<Membresia?> ObtenerActivaPorCliente(Guid clienteId)
    {
        var ahora = DateTime.UtcNow.AddHours(-4);

        return await _context.Membresias
            .Where(m => m.ClienteId == clienteId
                && m.Estado == "activa"
                && m.FechaFin >= ahora)
            .OrderByDescending(m => m.FechaFin)
            .FirstOrDefaultAsync();
    }

    public async Task<Membresia> Crear(Membresia membresia)
    {
        _context.Membresias.Add(membresia);
        await _context.SaveChangesAsync();
        return membresia;
    }

    public async Task<Membresia> Actualizar(Membresia membresia)
    {
        _context.Membresias.Update(membresia);
        await _context.SaveChangesAsync();
        return membresia;
    }
}
