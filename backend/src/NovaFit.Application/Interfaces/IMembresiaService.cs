using NovaFit.Application.DTOs;

namespace NovaFit.Application.Interfaces;

public interface IMembresiaService
{
    Task<IEnumerable<MembresiaDto>> ObtenerTodas();
    Task<MembresiaDto?> ObtenerPorId(Guid id);
    Task<MembresiaDto?> ObtenerActivaPorCliente(Guid clienteId);
    Task<MembresiaDto> Crear(CreateMembresiaDto dto);
    Task<bool> CancelarMembresia(Guid id);
}
