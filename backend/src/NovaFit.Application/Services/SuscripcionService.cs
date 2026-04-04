using NovaFit.Application.DTOs;
using NovaFit.Application.Interfaces;
using NovaFit.Domain.Entities;

namespace NovaFit.Application.Services;

public class SuscripcionService : ISuscripcionService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly ISuscripcionRepository _SuscripcionRepository;

    public SuscripcionService(
        ISuscripcionRepository SuscripcionRepository,
        IClienteRepository clienteRepository)
    {
        _SuscripcionRepository = SuscripcionRepository;
        _clienteRepository = clienteRepository;
    }

    public async Task<IEnumerable<SuscripcionDto>> ObtenerTodas()
    {
        var Suscripcions = await _SuscripcionRepository.ObtenerTodas();
        return Suscripcions.Select(MapearADto);
    }

    public async Task<SuscripcionDto?> ObtenerPorId(Guid id)
    {
        var Suscripcion = await _SuscripcionRepository.ObtenerPorId(id);
        return Suscripcion is null ? null : MapearADto(Suscripcion);
    }

    public async Task<SuscripcionDto?> ObtenerActivaPorCliente(Guid clienteId)
    {
        var Suscripcion = await _SuscripcionRepository.ObtenerActivaPorCliente(clienteId);
        return Suscripcion is null ? null : MapearADto(Suscripcion);
    }

    public async Task<SuscripcionDto> Crear(CreateSuscripcionDto dto)
    {
        if (dto.ClienteId == Guid.Empty)
            throw new InvalidOperationException("ClienteId es obligatorio");

        var cliente = await _clienteRepository.ObtenerPorId(dto.ClienteId);
        if (cliente is null)
            throw new InvalidOperationException("El cliente no existe");

        if (dto.Precio <= 0)
            throw new InvalidOperationException("El Precio debe ser mayor a 0");

        var Tipo = dto.Tipo.Trim().ToLowerInvariant();
        if (Tipo is not ("mensual" or "anual"))
            throw new InvalidOperationException("Tipo de plan invalido. Use mensual o anual");

        var ahora = DateTime.UtcNow.AddHours(-4);
        var Suscripcion = new Suscripcion
        {
            Id = Guid.NewGuid(),
            ClienteId = dto.ClienteId,
            Tipo = Tipo,
            Precio = dto.Precio,
            FechaInicio = ahora,
            FechaVencimiento = CalcularFechaVencimiento(Tipo, ahora),
            Estado = "activa",
            FechaCreacion = ahora
        };

        await _SuscripcionRepository.Crear(Suscripcion);
        return MapearADto(Suscripcion);
    }

    public async Task<bool> CancelarSuscripcion(Guid id)
    {
        var Suscripcion = await _SuscripcionRepository.ObtenerPorId(id);
        if (Suscripcion is null)
            return false;

        Suscripcion.Estado = "cancelada";
        await _SuscripcionRepository.Actualizar(Suscripcion);
        return true;
    }

    private static DateTime CalcularFechaVencimiento(string Tipo, DateTime fechaInicio)
    {
        return Tipo switch
        {
            "mensual" => fechaInicio.AddDays(30),
            "anual" => fechaInicio.AddDays(365),
            _ => fechaInicio
        };
    }

    private static SuscripcionDto MapearADto(Suscripcion Suscripcion)
    {
        return new SuscripcionDto
        {
            Id = Suscripcion.Id,
            ClienteId = Suscripcion.ClienteId,
            Tipo = Suscripcion.Tipo,
            Precio = Suscripcion.Precio,
            FechaInicio = Suscripcion.FechaInicio,
            FechaVencimiento = Suscripcion.FechaVencimiento,
            Estado = Suscripcion.Estado,
            EstaVigente = Suscripcion.EstaVigente()
        };
    }
}
