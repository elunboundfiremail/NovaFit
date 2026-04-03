using NovaFit.Application.DTOs;
using NovaFit.Application.Interfaces;
using NovaFit.Domain.Entities;

namespace NovaFit.Application.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _repository;

    public ClienteService(IClienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ClienteDto>> ObtenerTodos()
    {
        var clientes = await _repository.ObtenerTodos();
        return clientes.Select(MapearADto);
    }

    public async Task<ClienteDto?> ObtenerPorId(Guid id)
    {
        var cliente = await _repository.ObtenerPorId(id);
        return cliente != null ? MapearADto(cliente) : null;
    }

    public async Task<ClienteDto?> ObtenerPorCi(int ci)
    {
        var cliente = await _repository.ObtenerPorCi(ci);
        return cliente != null ? MapearADto(cliente) : null;
    }

    public async Task<ClienteDto> Crear(CreateClienteDto dto)
    {
        var existe = await _repository.ObtenerPorCi(dto.Ci);
        if (existe != null)
            throw new InvalidOperationException("Ya existe un cliente con ese CI");

        var cliente = new Cliente
        {
            Id = Guid.NewGuid(),
            Ci = dto.Ci,
            Nombre = dto.Nombre,
            Apellido = dto.Apellido,
            Email = dto.Email,
            Telefono = dto.Telefono,
            FechaNacimiento = dto.FechaNacimiento,
            FechaRegistro = DateTime.UtcNow.AddHours(-4)
        };

        await _repository.Crear(cliente);
        return MapearADto(cliente);
    }

    public async Task<ClienteDto?> Actualizar(Guid id, UpdateClienteDto dto)
    {
        var cliente = await _repository.ObtenerPorId(id);
        if (cliente == null) return null;

        if (dto.Nombre != null) cliente.Nombre = dto.Nombre;
        if (dto.Apellido != null) cliente.Apellido = dto.Apellido;
        if (dto.Email != null) cliente.Email = dto.Email;
        if (dto.Telefono != null) cliente.Telefono = dto.Telefono;
        if (dto.FechaNacimiento.HasValue) cliente.FechaNacimiento = dto.FechaNacimiento;

        await _repository.Actualizar(cliente);
        return MapearADto(cliente);
    }

    public async Task<bool> Eliminar(Guid id)
    {
        return await _repository.Eliminar(id);
    }

    private static ClienteDto MapearADto(Cliente cliente)
    {
        return new ClienteDto
        {
            Id = cliente.Id,
            Ci = cliente.Ci,
            Nombre = cliente.Nombre,
            Apellido = cliente.Apellido,
            Email = cliente.Email,
            Telefono = cliente.Telefono,
            FechaNacimiento = cliente.FechaNacimiento,
            FechaRegistro = cliente.FechaRegistro
        };
    }
}
