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
        if (dto.CasilleroId == Guid.Empty || dto.ClienteId == Guid.Empty)
            throw new InvalidOperationException("CasilleroId y ClienteId son obligatorios");

        if (string.IsNullOrWhiteSpace(dto.DocumentoRetenido))
            throw new InvalidOperationException("Documento retenido es obligatorio");

        var casillero = await _casilleroRepository.ObtenerPorId(dto.CasilleroId);
        if (casillero is null)
            throw new InvalidOperationException("Casillero no encontrado");

        if (!casillero.Activo)
            throw new InvalidOperationException("Casillero no operativo");

        if (await _casilleroRepository.TienePrestamoActivo(dto.CasilleroId))
            throw new InvalidOperationException("Casillero no disponible");

        var cliente = await _clienteRepository.ObtenerPorId(dto.ClienteId);
        if (cliente is null)
            throw new InvalidOperationException("Cliente no encontrado");

        var ahora = DateTime.UtcNow.AddHours(-4);
        var prestamo = new PrestamoCasillero
        {
            Id = Guid.NewGuid(),
            CasilleroId = dto.CasilleroId,
            ClienteId = dto.ClienteId,
            DocumentoRetenido = dto.DocumentoRetenido.Trim(),
            FechaHoraPrestamo = ahora,
            CreadoEn = ahora
        };

        await _casilleroRepository.CrearPrestamo(prestamo);

        casillero.Disponible = false;
        await _casilleroRepository.ActualizarCasillero(casillero);

        prestamo.Casillero = casillero;
        prestamo.Cliente = cliente;

        return MapearPrestamo(prestamo);
    }

    public async Task<PrestamoDto> DevolverCasillero(Guid prestamoId)
    {
        var prestamo = await _casilleroRepository.ObtenerPrestamoPorId(prestamoId);
        if (prestamo is null)
            throw new InvalidOperationException("Prestamo no encontrado");

        if (prestamo.FechaHoraDevolucion is not null)
            throw new InvalidOperationException("Ya fue devuelto");

        prestamo.FechaHoraDevolucion = DateTime.UtcNow.AddHours(-4);
        await _casilleroRepository.ActualizarPrestamo(prestamo);

        var casillero = await _casilleroRepository.ObtenerPorId(prestamo.CasilleroId);
        if (casillero is not null)
        {
            casillero.Disponible = true;
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
            Disponible = casillero.Disponible,
            Activo = casillero.Activo
        };
    }

    private static PrestamoDto MapearPrestamo(PrestamoCasillero prestamo)
    {
        return new PrestamoDto
        {
            Id = prestamo.Id,
            CasilleroId = prestamo.CasilleroId,
            ClienteId = prestamo.ClienteId,
            NumeroCasillero = prestamo.Casillero.Numero,
            NombreCliente = $"{prestamo.Cliente.Nombres} {prestamo.Cliente.ApellidoPaterno}".Trim(),
            DocumentoRetenido = prestamo.DocumentoRetenido,
            FechaHoraPrestamo = prestamo.FechaHoraPrestamo,
            FechaHoraDevolucion = prestamo.FechaHoraDevolucion,
            EstaActivo = prestamo.EstaActivo()
        };
    }
}
