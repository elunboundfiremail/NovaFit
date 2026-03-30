using NovaFit.Application.DTOs;

namespace NovaFit.Application.Interfaces;

public interface ICasilleroService
{
    Task<IEnumerable<CasilleroDto>> ObtenerTodos();
    Task<CasilleroDto?> ObtenerPorId(Guid id);
    Task<IEnumerable<CasilleroDto>> ObtenerDisponibles();
    Task<PrestamoDto> PrestarCasillero(PrestarCasilleroDto dto);
    Task<PrestamoDto> DevolverCasillero(Guid prestamoId);
    Task<IEnumerable<PrestamoDto>> ObtenerPrestamosActivos();
    Task<IEnumerable<PrestamoDto>> ObtenerHistorial(Guid casilleroId);
}
