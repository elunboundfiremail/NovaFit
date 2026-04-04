namespace NovaFit.Domain.Entities;

public class PromocionFestiva
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal PorcentajeDescuento { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public bool Activa { get; set; } = true;
    
    // Para reportes y análisis
    public int VecesAplicada { get; set; } = 0;
    
    // Auditoría y soft delete
    public DateTime FechaCreacion { get; set; }
    public bool Eliminado { get; set; } = false;
    public DateTime? FechaEliminacion { get; set; }

    // Relaciones
    public ICollection<Suscripcion> Suscripciones { get; set; } = new List<Suscripcion>();

    public bool EstaVigente()
    {
        var ahora = DateTime.UtcNow.AddHours(-4);
        return Activa && ahora >= FechaInicio && ahora <= FechaFin && !Eliminado;
    }
}
