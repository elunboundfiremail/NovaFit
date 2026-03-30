using NovaFit.Domain.Entities;

namespace NovaFit.Application.Interfaces;

public interface IIngresoRepository
{
    Task<Ingreso> Crear(Ingreso ingreso);
    Task<IEnumerable<Ingreso>> ObtenerTodos();
    Task<Ingreso?> ObtenerPorId(Guid id);
    Task<IEnumerable<Ingreso>> ObtenerPorCliente(Guid clienteId);
    Task<IEnumerable<Ingreso>> ObtenerRechazados();
}
