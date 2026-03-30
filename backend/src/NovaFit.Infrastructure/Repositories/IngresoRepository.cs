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
            .OrderByDescending(i => i.FechaHoraIngreso)
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
            .OrderByDescending(i => i.FechaHoraIngreso)
            .ToListAsync();
    }

    public async Task<IEnumerable<Ingreso>> ObtenerRechazados()
    {
        return await _context.Ingresos
            .Include(i => i.Cliente)
            .Where(i => !i.Permitido)
            .OrderByDescending(i => i.FechaHoraIngreso)
            .ToListAsync();
    }
}
