using NovaFit.Domain.Entities;

namespace NovaFit.Application.Interfaces;

public interface IClienteRepository
{
    Task<IEnumerable<Cliente>> ObtenerTodos();
    Task<Cliente?> ObtenerPorId(Guid id);
    Task<Cliente?> ObtenerPorCi(int ci);
    Task<Cliente> Crear(Cliente cliente);
    Task<Cliente> Actualizar(Cliente cliente);
    Task<bool> Eliminar(Guid id);
}
