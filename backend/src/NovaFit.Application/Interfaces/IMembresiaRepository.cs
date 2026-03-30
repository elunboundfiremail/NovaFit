using NovaFit.Domain.Entities;

namespace NovaFit.Application.Interfaces;

public interface IMembresiaRepository
{
    Task<IEnumerable<Membresia>> ObtenerTodas();
    Task<Membresia?> ObtenerPorId(Guid id);
    Task<Membresia?> ObtenerActivaPorCliente(Guid clienteId);
    Task<Membresia?> ObtenerUltimaPorCliente(Guid clienteId);
    Task<Membresia> Crear(Membresia membresia);
    Task<Membresia> Actualizar(Membresia membresia);
}
