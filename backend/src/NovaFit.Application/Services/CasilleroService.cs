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

    public async Task<CasilleroDto> CrearCasillero(CreateCasilleroDto dto)
    {
        ValidarCasillero(dto.Numero, dto.Tipo, dto.Estado);

        var existente = await _casilleroRepository.ObtenerPorNumero(dto.Numero);
        if (existente is not null)
            throw new InvalidOperationException("Ya existe un casillero con ese numero");

        var ahora = DateTime.UtcNow.AddHours(-4);
        var casillero = new Casillero
        {
            Id = Guid.NewGuid(),
            Numero = dto.Numero,
            Tipo = NormalizarTipo(dto.Tipo),
            Estado = NormalizarEstado(dto.Estado),
            Ubicacion = string.IsNullOrWhiteSpace(dto.Ubicacion) ? null : dto.Ubicacion.Trim(),
            FechaCreacion = ahora
        };

        await _casilleroRepository.CrearCasillero(casillero);
        return MapearCasillero(casillero);
    }

    public async Task<CasilleroDto?> ActualizarCasillero(Guid id, UpdateCasilleroDto dto)
    {
        var casillero = await _casilleroRepository.ObtenerPorId(id);
        if (casillero is null || casillero.Eliminado)
            return null;

        ValidarCasillero(dto.Numero, dto.Tipo, dto.Estado);

        var existente = await _casilleroRepository.ObtenerPorNumero(dto.Numero);
        if (existente is not null && existente.Id != id)
            throw new InvalidOperationException("Ya existe un casillero con ese numero");

        if (casillero.Estado == "OCUPADO" && NormalizarEstado(dto.Estado) != "OCUPADO")
        {
            if (await _casilleroRepository.TienePrestamoActivo(id))
                throw new InvalidOperationException("No se puede cambiar el estado de un casillero con prestamo activo");
        }

        casillero.Numero = dto.Numero;
        casillero.Tipo = NormalizarTipo(dto.Tipo);
        casillero.Estado = NormalizarEstado(dto.Estado);
        casillero.Ubicacion = string.IsNullOrWhiteSpace(dto.Ubicacion) ? null : dto.Ubicacion.Trim();

        await _casilleroRepository.ActualizarCasillero(casillero);
        return MapearCasillero(casillero);
    }

    public async Task<bool> EliminarCasillero(Guid id)
    {
        var casillero = await _casilleroRepository.ObtenerPorId(id);
        if (casillero is null || casillero.Eliminado)
            return false;

        if (casillero.Estado == "OCUPADO" || await _casilleroRepository.TienePrestamoActivo(id))
            throw new InvalidOperationException("No se puede eliminar un casillero ocupado o con prestamo activo");

        casillero.Eliminado = true;
        casillero.FechaEliminacion = DateTime.UtcNow.AddHours(-4);
        await _casilleroRepository.ActualizarCasillero(casillero);
        return true;
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

        if (await _casilleroRepository.TienePrestamoActivoPorIngreso(dto.IngresoId))
            throw new InvalidOperationException("El cliente ya tiene un resguardo activo");

        var ahora = DateTime.UtcNow.AddHours(-4);
        var numeroTicket = string.IsNullOrWhiteSpace(dto.NumeroTicket) ? null : dto.NumeroTicket.Trim();
        var numeroLlave = string.IsNullOrWhiteSpace(dto.NumeroLlave)
            ? (numeroTicket is null ? (await _casilleroRepository.ObtenerSiguienteNumeroLlave()).ToString() : null)
            : dto.NumeroLlave.Trim();

        var prestamo = new PrestamoCasillero
        {
            Id = Guid.NewGuid(),
            CasilleroId = dto.CasilleroId,
            IngresoId = dto.IngresoId,
            NumeroTicket = numeroTicket,
            NumeroLlave = numeroLlave,
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

    public async Task<PrestamoDto> RegistrarTicketRecepcion(RegistrarTicketRecepcionDto dto)
    {
        if (dto.IngresoId == Guid.Empty)
            throw new InvalidOperationException("IngresoId es obligatorio");

        if (await _casilleroRepository.TienePrestamoActivoPorIngreso(dto.IngresoId))
            throw new InvalidOperationException("El cliente ya tiene un resguardo activo");

        var estanteRecepcion = (await _casilleroRepository.ObtenerTodos())
            .Where(c => !c.Eliminado && c.Tipo == "ESTANTE_RECEPCION" && c.Estado == "DISPONIBLE")
            .OrderBy(c => c.Numero)
            .FirstOrDefault();

        if (estanteRecepcion is null)
            throw new InvalidOperationException("No hay tickets de recepcion disponibles");

        var ahora = DateTime.UtcNow.AddHours(-4);
        var siguienteTicket = await _casilleroRepository.ObtenerSiguienteNumeroTicket();
        var prestamo = new PrestamoCasillero
        {
            Id = Guid.NewGuid(),
            IngresoId = dto.IngresoId,
            CasilleroId = estanteRecepcion.Id,
            NumeroTicket = siguienteTicket.ToString(),
            NumeroLlave = string.IsNullOrWhiteSpace(dto.Descripcion) ? null : dto.Descripcion.Trim(),
            FechaPrestamo = ahora,
            HoraPrestamo = ahora.TimeOfDay,
            FechaCreacion = ahora
        };

        await _casilleroRepository.CrearPrestamo(prestamo);
        estanteRecepcion.Estado = "OCUPADO";
        await _casilleroRepository.ActualizarCasillero(estanteRecepcion);
        prestamo = await _casilleroRepository.ObtenerPrestamoPorId(prestamo.Id) ?? prestamo;

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

    private static void ValidarCasillero(int numero, string tipo, string estado)
    {
        if (numero <= 0)
            throw new InvalidOperationException("El numero del casillero debe ser mayor a 0");

        _ = NormalizarTipo(tipo);
        _ = NormalizarEstado(estado);
    }

    private static string NormalizarTipo(string tipo)
    {
        var tipoNormalizado = tipo.Trim().ToUpperInvariant();
        if (tipoNormalizado is not ("FIJO" or "TEMPORAL" or "ESTANTE_RECEPCION"))
            throw new InvalidOperationException("Tipo de casillero invalido");

        return tipoNormalizado;
    }

    private static string NormalizarEstado(string estado)
    {
        var estadoNormalizado = estado.Trim().ToUpperInvariant();
        if (estadoNormalizado == "ACTIVO")
            return "DISPONIBLE";

        if (estadoNormalizado is not ("DISPONIBLE" or "OCUPADO" or "MANTENIMIENTO" or "EN_MANTENIMIENTO"))
            throw new InvalidOperationException("Estado de casillero invalido");

        return estadoNormalizado == "EN_MANTENIMIENTO" ? "MANTENIMIENTO" : estadoNormalizado;
    }

    private static PrestamoDto MapearPrestamo(PrestamoCasillero prestamo)
    {
        var esTicketRecepcion = prestamo.Casillero?.Tipo == "ESTANTE_RECEPCION" && !string.IsNullOrWhiteSpace(prestamo.NumeroTicket);
        var identificadorResguardo = !string.IsNullOrWhiteSpace(prestamo.NumeroLlave)
            ? esTicketRecepcion ? prestamo.NumeroTicket : prestamo.NumeroLlave
            : prestamo.NumeroTicket;

        var tipoResguardo = esTicketRecepcion
            ? "TICKET"
            : !string.IsNullOrWhiteSpace(prestamo.NumeroLlave)
            ? "LLAVE"
            : !string.IsNullOrWhiteSpace(prestamo.NumeroTicket)
                ? "TICKET"
                : null;

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
            EstaActivo = prestamo.EstaActivo(),
            NombreCliente = prestamo.Ingreso?.Cliente is not null
                ? $"{prestamo.Ingreso.Cliente.Nombre} {prestamo.Ingreso.Cliente.Apellido}".Trim()
                : null,
            CiCliente = prestamo.Ingreso?.Cliente?.Ci,
            TipoResguardo = tipoResguardo,
            IdentificadorResguardo = identificadorResguardo,
            Descripcion = esTicketRecepcion ? prestamo.NumeroLlave : null
        };
    }
}
