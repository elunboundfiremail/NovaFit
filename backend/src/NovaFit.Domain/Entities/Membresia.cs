namespace NovaFit.Domain.Entities;

public class Membresia
{
    public Guid Id { get; set; }
    public Guid ClienteId { get; set; }
    public string TipoPlan { get; set; } = "mensual";
    public decimal Costo { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public string Estado { get; set; } = "activa";
    public string? Observacion { get; set; }
    public DateTime CreadoEn { get; set; }

    public Cliente Cliente { get; set; } = null!;
    public ICollection<Ingreso> Ingresos { get; set; } = new List<Ingreso>();

    public bool EstaVigente()
    {
        var ahora = DateTime.UtcNow.AddHours(-4);
        return FechaFin >= ahora && Estado == "activa";
    }
}
