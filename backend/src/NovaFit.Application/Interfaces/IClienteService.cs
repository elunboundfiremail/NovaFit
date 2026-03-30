using NovaFit.Application.DTOs;

namespace NovaFit.Application.Interfaces;

public interface IClienteService
{
    Task<IEnumerable<ClienteDto>> ObtenerTodos();
    Task<ClienteDto?> ObtenerPorId(Guid id);
    Task<ClienteDto?> ObtenerPorCi(int ci);
    Task<ClienteDto> Crear(CreateClienteDto dto);
    Task<ClienteDto?> Actualizar(Guid id, UpdateClienteDto dto);
    Task<bool> Eliminar(Guid id);
}
