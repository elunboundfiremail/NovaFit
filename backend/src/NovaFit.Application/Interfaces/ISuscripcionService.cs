using NovaFit.Application.DTOs;

namespace NovaFit.Application.Interfaces;

public interface ISuscripcionService
{
    Task<IEnumerable<SuscripcionDto>> ObtenerTodas();
    Task<SuscripcionDto?> ObtenerPorId(Guid id);
    Task<SuscripcionDto?> ObtenerActivaPorCliente(Guid clienteId);
    Task<SuscripcionDto> Crear(CreateSuscripcionDto dto);
    Task<SuscripcionDto?> Actualizar(Guid id, UpdateSuscripcionDto dto);
    Task<bool> CancelarSuscripcion(Guid id);
    Task<bool> Eliminar(Guid id);
}
