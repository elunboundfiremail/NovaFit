using NovaFit.Domain.Entities;

namespace NovaFit.Application.Interfaces;

public interface ICasilleroRepository
{
    Task<IEnumerable<Casillero>> ObtenerTodos();
    Task<IEnumerable<Casillero>> ObtenerDisponibles();
    Task<Casillero?> ObtenerPorId(Guid id);
    Task<Casillero?> ObtenerPorNumero(int numero);
    Task<Casillero> CrearCasillero(Casillero casillero);
    Task<Casillero> ActualizarCasillero(Casillero casillero);
    Task<PrestamoCasillero> CrearPrestamo(PrestamoCasillero prestamo);
    Task<PrestamoCasillero?> ObtenerPrestamoPorId(Guid id);
    Task<PrestamoCasillero> ActualizarPrestamo(PrestamoCasillero prestamo);
    Task<IEnumerable<PrestamoCasillero>> ObtenerPrestamosActivos();
    Task<IEnumerable<PrestamoCasillero>> ObtenerHistorialPorCasillero(Guid casilleroId);
    Task<bool> TienePrestamoActivo(Guid casilleroId);
    Task<bool> TienePrestamoActivoPorIngreso(Guid ingresoId);
}
