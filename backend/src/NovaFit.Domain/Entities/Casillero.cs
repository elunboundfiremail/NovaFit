namespace NovaFit.Domain.Entities;

public class Casillero
{
    public Guid Id { get; set; }
    public int Numero { get; set; }
    public string Tipo { get; set; } = "TEMPORAL"; // FIJO | TEMPORAL | ESTANTE_RECEPCION
    public string? Ubicacion { get; set; }
    public string Estado { get; set; } = "DISPONIBLE"; // DISPONIBLE | OCUPADO | EN_MANTENIMIENTO
    
    // Para casilleros FIJOS (solo ANUAL premium)
    public Guid? AsignadoAClienteId { get; set; }
    
    // Auditoría y soft delete
    public DateTime FechaCreacion { get; set; }
    public bool Eliminado { get; set; } = false;
    public DateTime? FechaEliminacion { get; set; }

    // Relaciones
    public Cliente? AsignadoACliente { get; set; }
    public ICollection<PrestamoCasillero> Prestamos { get; set; } = new List<PrestamoCasillero>();
    public ICollection<Suscripcion> SuscripcionesConCasilleroFijo { get; set; } = new List<Suscripcion>();
}
