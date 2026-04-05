using Microsoft.EntityFrameworkCore;
using NovaFit.Application.Interfaces;
using NovaFit.Domain.Entities;
using NovaFit.Infrastructure.Data;

namespace NovaFit.Infrastructure.Repositories;

public class IngresoRepository : IIngresoRepository
{
    private readonly NovaFitDbContext _context;

    public IngresoRepository(NovaFitDbContext context)
    {
        _context = context;
    }

    public async Task<Ingreso> Crear(Ingreso ingreso)
    {
        _context.Ingresos.Add(ingreso);
        await _context.SaveChangesAsync();
        return ingreso;
    }

    public async Task<IEnumerable<Ingreso>> ObtenerTodos()
    {
        return await _context.Ingresos
            .Include(i => i.Cliente)
            .ToListAsync();
    }

    public async Task<Ingreso?> ObtenerPorId(Guid id)
    {
        return await _context.Ingresos
            .Include(i => i.Cliente)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IEnumerable<Ingreso>> ObtenerPorCliente(Guid clienteId)
    {
        return await _context.Ingresos
            .Include(i => i.Cliente)
            .Where(i => i.ClienteId == clienteId)
            .ToListAsync();
    }

    public async Task<Ingreso?> ObtenerActivoPorCliente(Guid clienteId)
    {
        return await _context.Ingresos
            .Include(i => i.Cliente)
            .Where(i => i.ClienteId == clienteId && !i.SalidaRegistrada)
            .OrderByDescending(i => i.FechaIngreso)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Ingreso>> ObtenerRechazados()
    {
        return await _context.Ingresos
            .Include(i => i.Cliente)
            .ToListAsync();
    }

    public async Task<Ingreso> Actualizar(Ingreso ingreso)
    {
        _context.Ingresos.Update(ingreso);
        await _context.SaveChangesAsync();
        return ingreso;
    }
}


