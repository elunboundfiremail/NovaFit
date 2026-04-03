namespace NovaFit.Domain.Entities;

public class PrestamoCasillero
{
    public Guid Id { get; set; }
    public Guid IngresoId { get; set; }
    public Guid? CasilleroId { get; set; }
    
    // Sistema de tickets (para CASUAL en estantes recepción)
    public string? NumeroTicket { get; set; }
    public int? CiDepositado { get; set; }
    
    // Sistema de llaves (para MENSUAL/ANUAL en casilleros)
    public string? NumeroLlave { get; set; }
    
    // Control de devolución
    public DateTime FechaPrestamo { get; set; }
    public TimeSpan HoraPrestamo { get; set; }
    public DateTime? FechaDevolucion { get; set; }
    public TimeSpan? HoraDevolucion { get; set; }
    public bool Devuelto { get; set; } = false;
    
    // Auditoría y soft delete
    public DateTime FechaCreacion { get; set; }
    public bool Eliminado { get; set; } = false;
    public DateTime? FechaEliminacion { get; set; }

    // Relaciones
    public Ingreso Ingreso { get; set; } = null!;
    public Casillero? Casillero { get; set; }

    public bool EstaActivo() => !Devuelto && !Eliminado;
}
