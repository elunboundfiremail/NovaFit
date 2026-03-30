namespace NovaFit.Application.DTOs;

public class MembresiaDto
{
    public Guid Id { get; set; }
    public Guid ClienteId { get; set; }
    public string TipoPlan { get; set; } = string.Empty;
    public decimal Costo { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public string Estado { get; set; } = string.Empty;
    public string? Observacion { get; set; }
    public bool EstaVigente { get; set; }
}

public class CreateMembresiaDto
{
    public Guid ClienteId { get; set; }
    public string TipoPlan { get; set; } = "mensual";
    public decimal Costo { get; set; }
    public string? Observacion { get; set; }
}
