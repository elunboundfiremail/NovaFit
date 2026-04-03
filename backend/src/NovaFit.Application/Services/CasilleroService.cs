using NovaFit.Application.DTOs;
using NovaFit.Application.Interfaces;
using NovaFit.Domain.Entities;

namespace NovaFit.Application.Services;

public class CasilleroService : ICasilleroService
{
    private readonly ICasilleroRepository _casilleroRepository;
    private readonly IClienteRepository _clienteRepository;

    public CasilleroService(
        ICasilleroRepository casilleroRepository,
        IClienteRepository clienteRepository)
    {
        _casilleroRepository = casilleroRepository;
        _clienteRepository = clienteRepository;
    }

    public async Task<IEnumerable<CasilleroDto>> ObtenerTodos()
    {
        var casilleros = await _casilleroRepository.ObtenerTodos();
        return casilleros.Select(MapearCasillero);
    }

    public async Task<CasilleroDto?> ObtenerPorId(Guid id)
    {
        var casillero = await _casilleroRepository.ObtenerPorId(id);
        return casillero is null ? null : MapearCasillero(casillero);
    }

    public async Task<IEnumerable<CasilleroDto>> ObtenerDisponibles()
    {
        var casilleros = await _casilleroRepository.ObtenerDisponibles();
        return casilleros.Select(MapearCasillero);
    }

    public async Task<PrestamoDto> PrestarCasillero(PrestarCasilleroDto dto)
    {
        if (dto.CasilleroId == Guid.Empty || dto.IngresoId == Guid.Empty)
            throw new InvalidOperationException("CasilleroId e IngresoId son obligatorios");

        var casillero = await _casilleroRepository.ObtenerPorId(dto.CasilleroId);
        if (casillero is null)
            throw new InvalidOperationException("Casillero no encontrado");

        if (casillero.Estado != "DISPONIBLE")
            throw new InvalidOperationException("Casillero no disponible");

        if (await _casilleroRepository.TienePrestamoActivo(dto.CasilleroId))
            throw new InvalidOperationException("Casillero ya está en uso");

        var ahora = DateTime.UtcNow.AddHours(-4);
        var prestamo = new PrestamoCasillero
        {
            Id = Guid.NewGuid(),
            CasilleroId = dto.CasilleroId,
            IngresoId = dto.IngresoId,
            NumeroTicket = dto.NumeroTicket,
            CiDepositado = dto.CiDepositado,
            FechaPrestamo = ahora,
            HoraPrestamo = ahora.TimeOfDay,
            FechaCreacion = ahora
        };

        await _casilleroRepository.CrearPrestamo(prestamo);

        casillero.Estado = "OCUPADO";
        await _casilleroRepository.ActualizarCasillero(casillero);

        prestamo.Casillero = casillero;

        return MapearPrestamo(prestamo);
    }

    public async Task<PrestamoDto> DevolverCasillero(Guid prestamoId)
    {
        var prestamo = await _casilleroRepository.ObtenerPrestamoPorId(prestamoId);
        if (prestamo is null)
            throw new InvalidOperationException("Prestamo no encontrado");

        if (prestamo.FechaDevolucion is not null)
            throw new InvalidOperationException("Ya fue devuelto");

        var ahora = DateTime.UtcNow.AddHours(-4);
        prestamo.FechaDevolucion = ahora;
        prestamo.HoraDevolucion = ahora.TimeOfDay;
        prestamo.Devuelto = true;
        await _casilleroRepository.ActualizarPrestamo(prestamo);

        var casillero = await _casilleroRepository.ObtenerPorId(prestamo.CasilleroId ?? Guid.Empty);
        if (casillero is not null)
        {
            casillero.Estado = "DISPONIBLE";
            await _casilleroRepository.ActualizarCasillero(casillero);
            prestamo.Casillero = casillero;
        }

        return MapearPrestamo(prestamo);
    }

    public async Task<IEnumerable<PrestamoDto>> ObtenerPrestamosActivos()
    {
        var prestamos = await _casilleroRepository.ObtenerPrestamosActivos();
        return prestamos.Select(MapearPrestamo);
    }

    public async Task<IEnumerable<PrestamoDto>> ObtenerHistorial(Guid casilleroId)
    {
        var prestamos = await _casilleroRepository.ObtenerHistorialPorCasillero(casilleroId);
        return prestamos.Select(MapearPrestamo);
    }

    private static CasilleroDto MapearCasillero(Casillero casillero)
    {
        return new CasilleroDto
        {
            Id = casillero.Id,
            Numero = casillero.Numero,
            Tipo = casillero.Tipo,
            Estado = casillero.Estado,
            Ubicacion = casillero.Ubicacion
        };
    }

    private static PrestamoDto MapearPrestamo(PrestamoCasillero prestamo)
    {
        return new PrestamoDto
        {
            Id = prestamo.Id,
            CasilleroId = prestamo.CasilleroId,
            IngresoId = prestamo.IngresoId,
            NumeroCasillero = prestamo.Casillero?.Numero ?? 0,
            NumeroTicket = prestamo.NumeroTicket,
            NumeroLlave = prestamo.NumeroLlave,
            CiDepositado = prestamo.CiDepositado,
            FechaPrestamo = prestamo.FechaPrestamo,
            FechaDevolucion = prestamo.FechaDevolucion,
            Devuelto = prestamo.Devuelto,
            EstaActivo = prestamo.EstaActivo()
        };
    }
}
