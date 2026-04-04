namespace NovaFit.Domain.Entities;

public class Suscripcion
{
    public Guid Id { get; set; }
    public Guid ClienteId { get; set; }
    
    // Tipo de suscripción
    public string Tipo { get; set; } = "MENSUAL"; // CASUAL | MENSUAL | ANUAL
    
    // Detalles de suscripción
    public decimal Precio { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaVencimiento { get; set; }
    public string Estado { get; set; } = "ACTIVA"; // ACTIVA | VENCIDA | CANCELADA
    
    // Para tipo ANUAL (Premium)
    public Guid? CasilleroFijoId { get; set; }
    
    // Promoción aplicada
    public Guid? PromocionId { get; set; }
    public decimal DescuentoAplicado { get; set; } = 0.00m;
    
    // Contador de ingresos
    public int IngresosTotalesUsados { get; set; } = 0;
    
    // Auditoría y soft delete
    public DateTime FechaCreacion { get; set; }
    public bool Eliminado { get; set; } = false;
    public DateTime? FechaEliminacion { get; set; }

    // Relaciones
    public Cliente Cliente { get; set; } = null!;
    public Casillero? CasilleroFijo { get; set; }
    public PromocionFestiva? Promocion { get; set; }
    public ICollection<Ingreso> Ingresos { get; set; } = new List<Ingreso>();

    public bool EstaVigente()
    {
        var ahora = DateTime.UtcNow.AddHours(-4);
        return FechaVencimiento >= ahora && Estado == "ACTIVA" && !Eliminado;
    }
}
