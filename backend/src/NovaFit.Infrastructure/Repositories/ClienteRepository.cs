using Microsoft.EntityFrameworkCore;
using NovaFit.Application.Interfaces;
using NovaFit.Domain.Entities;
using NovaFit.Infrastructure.Data;

namespace NovaFit.Infrastructure.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly NovaFitDbContext _context;

    public ClienteRepository(NovaFitDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cliente>> ObtenerTodos()
    {
        return await _context.Clientes
            .Where(c => !c.Eliminado)
            .OrderBy(c => c.Apellido)
            .ToListAsync();
    }

    public async Task<Cliente?> ObtenerPorId(Guid id)
    {
        return await _context.Clientes.FindAsync(id);
    }

    public async Task<Cliente?> ObtenerPorCi(int ci)
    {
        return await _context.Clientes
            .FirstOrDefaultAsync(c => c.Ci == ci);
    }

    public async Task<Cliente> Crear(Cliente cliente)
    {
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();
        return cliente;
    }

    public async Task<Cliente> Actualizar(Cliente cliente)
    {
        _context.Clientes.Update(cliente);
        await _context.SaveChangesAsync();
        return cliente;
    }

    public async Task<bool> Eliminar(Guid id)
    {
        var cliente = await ObtenerPorId(id);
        if (cliente == null) return false;

        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync();
        return true;
    }
}
