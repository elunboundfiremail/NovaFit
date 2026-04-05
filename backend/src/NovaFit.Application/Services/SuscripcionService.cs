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
        ValidarDatos(dto.ClienteId, dto.Tipo, dto.Precio);

        var cliente = await _clienteRepository.ObtenerPorId(dto.ClienteId);
        if (cliente is null)
            throw new InvalidOperationException("El cliente no existe");

        var tipo = NormalizarTipo(dto.Tipo);
        var ahora = DateTime.UtcNow.AddHours(-4);
        var Suscripcion = new Suscripcion
        {
            Id = Guid.NewGuid(),
            ClienteId = dto.ClienteId,
            Tipo = tipo,
            Precio = dto.Precio,
            FechaInicio = ahora,
            FechaVencimiento = CalcularFechaVencimiento(tipo, ahora),
            Estado = "activa",
            FechaCreacion = ahora
        };

        await _SuscripcionRepository.Crear(Suscripcion);
        return MapearADto(Suscripcion);
    }

    public async Task<SuscripcionDto?> Actualizar(Guid id, UpdateSuscripcionDto dto)
    {
        var Suscripcion = await _SuscripcionRepository.ObtenerPorId(id);
        if (Suscripcion is null)
            return null;

        ValidarDatos(dto.ClienteId, dto.Tipo, dto.Precio);

        if (dto.ClienteId == Guid.Empty)
            throw new InvalidOperationException("ClienteId es obligatorio");

        var cliente = await _clienteRepository.ObtenerPorId(dto.ClienteId);
        if (cliente is null)
            throw new InvalidOperationException("El cliente no existe");

        var tipo = NormalizarTipo(dto.Tipo);
        Suscripcion.ClienteId = dto.ClienteId;
        Suscripcion.Tipo = tipo;
        Suscripcion.Precio = dto.Precio;
        Suscripcion.FechaVencimiento = CalcularFechaVencimiento(tipo, Suscripcion.FechaInicio);

        if (Suscripcion.Eliminado)
        {
            Suscripcion.Eliminado = false;
            Suscripcion.FechaEliminacion = null;
        }

        if (Suscripcion.FechaVencimiento >= DateTime.UtcNow.AddHours(-4) && !string.Equals(Suscripcion.Estado, "cancelada", StringComparison.OrdinalIgnoreCase))
            Suscripcion.Estado = "activa";

        await _SuscripcionRepository.Actualizar(Suscripcion);
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

    public async Task<bool> Eliminar(Guid id)
    {
        var Suscripcion = await _SuscripcionRepository.ObtenerPorId(id);
        if (Suscripcion is null)
            return false;

        var ahora = DateTime.UtcNow.AddHours(-4);
        Suscripcion.Eliminado = true;
        Suscripcion.FechaEliminacion = ahora;
        Suscripcion.Estado = "cancelada";
        await _SuscripcionRepository.Actualizar(Suscripcion);
        return true;
    }

    private static void ValidarDatos(Guid clienteId, string tipo, decimal precio)
    {
        if (clienteId == Guid.Empty)
            throw new InvalidOperationException("ClienteId es obligatorio");

        if (precio <= 0)
            throw new InvalidOperationException("El Precio debe ser mayor a 0");

        _ = NormalizarTipo(tipo);
    }

    private static string NormalizarTipo(string tipo)
    {
        var tipoNormalizado = tipo.Trim().ToLowerInvariant();
        if (tipoNormalizado is not ("casual" or "mensual" or "anual"))
            throw new InvalidOperationException("Tipo de plan invalido. Use casual, mensual o anual");

        return tipoNormalizado;
    }

    private static DateTime CalcularFechaVencimiento(string Tipo, DateTime fechaInicio)
    {
        return Tipo switch
        {
            "casual" => fechaInicio.AddDays(1),
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
