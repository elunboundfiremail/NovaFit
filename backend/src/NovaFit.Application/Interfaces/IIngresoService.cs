using NovaFit.Application.DTOs;

namespace NovaFit.Application.Interfaces;

public interface IIngresoService
{
    Task<IEnumerable<IngresoDto>> ObtenerTodos();
    Task<IngresoDto?> ObtenerPorId(Guid id);
    Task<IEnumerable<IngresoDto>> ObtenerPorCliente(Guid clienteId);
    Task<IEnumerable<IngresoDto>> ObtenerRechazados();
    Task<IngresoDto> RegistrarIngreso(int ci);
}
