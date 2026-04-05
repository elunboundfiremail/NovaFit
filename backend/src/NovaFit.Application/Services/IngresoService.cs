using NovaFit.Application.DTOs;
using NovaFit.Application.Interfaces;
using NovaFit.Domain.Entities;

namespace NovaFit.Application.Services;

public class IngresoService : IIngresoService
{
    private readonly IIngresoRepository _ingresoRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly ISuscripcionRepository _SuscripcionRepository;
    private readonly ICasilleroRepository _casilleroRepository;

    public IngresoService(
        IIngresoRepository ingresoRepository,
        IClienteRepository clienteRepository,
        ISuscripcionRepository SuscripcionRepository,
        ICasilleroRepository casilleroRepository)
    {
        _ingresoRepository = ingresoRepository;
        _clienteRepository = clienteRepository;
        _SuscripcionRepository = SuscripcionRepository;
        _casilleroRepository = casilleroRepository;
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

        var ingresoActivo = await _ingresoRepository.ObtenerActivoPorCliente(cliente.Id);
        if (ingresoActivo is not null)
            throw new InvalidOperationException("El cliente ya tiene un ingreso activo");

        var ahora = DateTime.UtcNow.AddHours(-4);
        var ultimaSuscripcion = await _SuscripcionRepository.ObtenerUltimaPorCliente(cliente.Id);

        var ingreso = new Ingreso
        {
            Id = Guid.NewGuid(),
            ClienteId = cliente.Id,
            FechaIngreso = ahora, HoraIngreso = ahora.TimeOfDay,
            FechaCreacion = ahora
        };

        if (ultimaSuscripcion is null)
        {
            
            
        }
        else if (!string.Equals(ultimaSuscripcion.Estado, "activa", StringComparison.OrdinalIgnoreCase))
        {
            
            
        }
        else if (!ultimaSuscripcion.EstaVigente())
        {
            
            
        }
        else
        {
            
            ingreso.SuscripcionId = ultimaSuscripcion.Id;
        }

        await _ingresoRepository.Crear(ingreso);

        ingreso.Suscripcion = ingreso.SuscripcionId.HasValue ? ultimaSuscripcion : null;

        return MapearADto(ingreso);
    }

    public async Task<IngresoDto?> RegistrarSalida(Guid ingresoId)
    {
        var ingreso = await _ingresoRepository.ObtenerPorId(ingresoId);
        if (ingreso is null)
            return null;

        if (await _casilleroRepository.TienePrestamoActivoPorIngreso(ingresoId))
            throw new InvalidOperationException("Debe devolver la llave o casillero antes de registrar salida");

        if (!ingreso.SalidaRegistrada)
        {
            var ahora = DateTime.UtcNow.AddHours(-4);
            ingreso.HoraSalida = ahora.TimeOfDay;
            ingreso.SalidaRegistrada = true;
            ingreso.DuracionMinutos = (int)Math.Max(0, (ahora.TimeOfDay - ingreso.HoraIngreso).TotalMinutes);

            await _ingresoRepository.Actualizar(ingreso);
        }

        return MapearADto(ingreso);
    }

    private static IngresoDto MapearADto(Ingreso ingreso)
    {
        return new IngresoDto
        {
            Id = ingreso.Id,
            ClienteId = ingreso.ClienteId,
            SuscripcionId = ingreso.SuscripcionId,
            FechaIngreso = ingreso.FechaIngreso,
            HoraIngreso = ingreso.HoraIngreso,
            HoraSalida = ingreso.HoraSalida,
            SalidaRegistrada = ingreso.SalidaRegistrada,
            DuracionMinutos = ingreso.DuracionMinutos,
            NombreCliente = ingreso.Cliente is null ? string.Empty : $"{ingreso.Cliente.Nombre} {ingreso.Cliente.Apellido}".Trim(),
            CiCliente = ingreso.Cliente?.Ci ?? 0
        };
    }
}
