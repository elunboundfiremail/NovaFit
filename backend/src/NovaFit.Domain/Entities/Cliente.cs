namespace NovaFit.Domain.Entities;

public class Cliente
{
    public Guid Id { get; set; }
    public int Ci { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Telefono { get; set; }
    public DateTime? FechaNacimiento { get; set; }
    
    // Auditoría y soft delete
    public DateTime FechaRegistro { get; set; }
    public bool Eliminado { get; set; } = false;
    public DateTime? FechaEliminacion { get; set; }

    // Relaciones
    public ICollection<Suscripcion> Suscripciones { get; set; } = new List<Suscripcion>();
    public ICollection<Ingreso> Ingresos { get; set; } = new List<Ingreso>();
    public ICollection<PrestamoCasillero> PrestamosCasilleros { get; set; } = new List<PrestamoCasillero>();
}
