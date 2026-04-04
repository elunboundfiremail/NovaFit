using NovaFit.Domain.Entities;

namespace NovaFit.Application.Interfaces;

public interface ISuscripcionRepository
{
    Task<IEnumerable<Suscripcion>> ObtenerTodas();
    Task<Suscripcion?> ObtenerPorId(Guid id);
    Task<Suscripcion?> ObtenerActivaPorCliente(Guid clienteId);
    Task<Suscripcion?> ObtenerUltimaPorCliente(Guid clienteId);
    Task<Suscripcion> Crear(Suscripcion Suscripcion);
    Task<Suscripcion> Actualizar(Suscripcion Suscripcion);
}
