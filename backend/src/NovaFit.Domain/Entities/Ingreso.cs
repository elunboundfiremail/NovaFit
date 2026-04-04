namespace NovaFit.Domain.Entities;

public class Ingreso
{
    public Guid Id { get; set; }
    public Guid ClienteId { get; set; }
    public Guid? SuscripcionId { get; set; }
    
    // Fechas y horas separadas
    public DateTime FechaIngreso { get; set; }
    public TimeSpan HoraIngreso { get; set; }
    public TimeSpan? HoraSalida { get; set; }
    
    // Control de salida
    public bool SalidaRegistrada { get; set; } = false;
    
    // Métricas de sesión
    public int? DuracionMinutos { get; set; }
    
    // Auditoría y soft delete
    public DateTime FechaCreacion { get; set; }
    public bool Eliminado { get; set; } = false;
    public DateTime? FechaEliminacion { get; set; }

    // Relaciones
    public Cliente Cliente { get; set; } = null!;
    public Suscripcion? Suscripcion { get; set; }
    public ICollection<PrestamoCasillero> PrestamosCasilleros { get; set; } = new List<PrestamoCasillero>();
}
