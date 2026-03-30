using NovaFit.Application.DTOs;
using NovaFit.Application.Interfaces;
using NovaFit.Domain.Entities;

namespace NovaFit.Application.Services;

public class IngresoService : IIngresoService
{
    private readonly IIngresoRepository _ingresoRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IMembresiaRepository _membresiaRepository;

    public IngresoService(
        IIngresoRepository ingresoRepository,
        IClienteRepository clienteRepository,
        IMembresiaRepository membresiaRepository)
    {
        _ingresoRepository = ingresoRepository;
        _clienteRepository = clienteRepository;
        _membresiaRepository = membresiaRepository;
    }

    public async Task<IEnumerable<IngresoDto>> ObtenerTodos()
    {
        var ingresos = await _ingresoRepository.ObtenerTodos();
        return ingresos.Select(MapearADto);
    }

    public async Task<IngresoDto?> ObtenerPorId(Guid id)
    {
        var ingreso = await _ingresoRepository.ObtenerPorId(id);
        return ingreso is null ? null : MapearADto(ingreso);
    }

    public async Task<IEnumerable<IngresoDto>> ObtenerPorCliente(Guid clienteId)
    {
        var ingresos = await _ingresoRepository.ObtenerPorCliente(clienteId);
        return ingresos.Select(MapearADto);
    }

    public async Task<IEnumerable<IngresoDto>> ObtenerRechazados()
    {
        var ingresos = await _ingresoRepository.ObtenerRechazados();
        return ingresos.Select(MapearADto);
    }

    public async Task<IngresoDto> RegistrarIngreso(int ci)
    {
        var cliente = await _clienteRepository.ObtenerPorCi(ci);
        if (cliente is null)
            throw new InvalidOperationException("Cliente no encontrado");

        var ahora = DateTime.UtcNow.AddHours(-4);
        var ultimaMembresia = await _membresiaRepository.ObtenerUltimaPorCliente(cliente.Id);

        var ingreso = new Ingreso
        {
            Id = Guid.NewGuid(),
            ClienteId = cliente.Id,
            FechaHoraIngreso = ahora,
            CreadoEn = ahora
        };

        if (ultimaMembresia is null)
        {
            ingreso.Permitido = false;
            ingreso.MotivoAlerta = "No tiene membresia";
        }
        else if (!string.Equals(ultimaMembresia.Estado, "activa", StringComparison.OrdinalIgnoreCase))
        {
            ingreso.Permitido = false;
            ingreso.MotivoAlerta = $"Membresia {ultimaMembresia.Estado}";
        }
        else if (!ultimaMembresia.EstaVigente())
        {
            ingreso.Permitido = false;
            ingreso.MotivoAlerta = $"Membresia vencida desde {ultimaMembresia.FechaFin:dd/MM/yyyy}";
        }
        else
        {
            ingreso.Permitido = true;
            ingreso.MembresiaId = ultimaMembresia.Id;
        }

        await _ingresoRepository.Crear(ingreso);

        ingreso.Cliente = cliente;
        ingreso.Membresia = ingreso.MembresiaId.HasValue ? ultimaMembresia : null;

        return MapearADto(ingreso);
    }

    private static IngresoDto MapearADto(Ingreso ingreso)
    {
        return new IngresoDto
        {
            Id = ingreso.Id,
            ClienteId = ingreso.ClienteId,
            MembresiaId = ingreso.MembresiaId,
            FechaHoraIngreso = ingreso.FechaHoraIngreso,
            Permitido = ingreso.Permitido,
            MotivoAlerta = ingreso.MotivoAlerta,
            NombreCliente = $"{ingreso.Cliente.Nombres} {ingreso.Cliente.ApellidoPaterno}".Trim(),
            CiCliente = ingreso.Cliente.Ci
        };
    }
}
