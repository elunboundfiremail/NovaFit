using NovaFit.Application.DTOs;
using NovaFit.Application.Interfaces;
using NovaFit.Domain.Entities;

namespace NovaFit.Application.Services;

public class MembresiaService : IMembresiaService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IMembresiaRepository _membresiaRepository;

    public MembresiaService(
        IMembresiaRepository membresiaRepository,
        IClienteRepository clienteRepository)
    {
        _membresiaRepository = membresiaRepository;
        _clienteRepository = clienteRepository;
    }

    public async Task<IEnumerable<MembresiaDto>> ObtenerTodas()
    {
        var membresias = await _membresiaRepository.ObtenerTodas();
        return membresias.Select(MapearADto);
    }

    public async Task<MembresiaDto?> ObtenerPorId(Guid id)
    {
        var membresia = await _membresiaRepository.ObtenerPorId(id);
        return membresia is null ? null : MapearADto(membresia);
    }

    public async Task<MembresiaDto?> ObtenerActivaPorCliente(Guid clienteId)
    {
        var membresia = await _membresiaRepository.ObtenerActivaPorCliente(clienteId);
        return membresia is null ? null : MapearADto(membresia);
    }

    public async Task<MembresiaDto> Crear(CreateMembresiaDto dto)
    {
        if (dto.ClienteId == Guid.Empty)
            throw new InvalidOperationException("ClienteId es obligatorio");

        var cliente = await _clienteRepository.ObtenerPorId(dto.ClienteId);
        if (cliente is null)
            throw new InvalidOperationException("El cliente no existe");

        if (dto.Costo <= 0)
            throw new InvalidOperationException("El costo debe ser mayor a 0");

        var tipoPlan = dto.TipoPlan.Trim().ToLowerInvariant();
        if (tipoPlan is not ("mensual" or "anual"))
            throw new InvalidOperationException("Tipo de plan invalido. Use mensual o anual");

        var ahora = DateTime.UtcNow.AddHours(-4);
        var membresia = new Membresia
        {
            Id = Guid.NewGuid(),
            ClienteId = dto.ClienteId,
            TipoPlan = tipoPlan,
            Costo = dto.Costo,
            FechaInicio = ahora,
            FechaFin = CalcularFechaFin(tipoPlan, ahora),
            Estado = "activa",
            Observacion = dto.Observacion,
            CreadoEn = ahora
        };

        await _membresiaRepository.Crear(membresia);
        return MapearADto(membresia);
    }

    public async Task<bool> CancelarMembresia(Guid id)
    {
        var membresia = await _membresiaRepository.ObtenerPorId(id);
        if (membresia is null)
            return false;

        membresia.Estado = "cancelada";
        await _membresiaRepository.Actualizar(membresia);
        return true;
    }

    private static DateTime CalcularFechaFin(string tipoPlan, DateTime fechaInicio)
    {
        return tipoPlan switch
        {
            "mensual" => fechaInicio.AddDays(30),
            "anual" => fechaInicio.AddDays(365),
            _ => fechaInicio
        };
    }

    private static MembresiaDto MapearADto(Membresia membresia)
    {
        return new MembresiaDto
        {
            Id = membresia.Id,
            ClienteId = membresia.ClienteId,
            TipoPlan = membresia.TipoPlan,
            Costo = membresia.Costo,
            FechaInicio = membresia.FechaInicio,
            FechaFin = membresia.FechaFin,
            Estado = membresia.Estado,
            Observacion = membresia.Observacion,
            EstaVigente = membresia.EstaVigente()
        };
    }
}
