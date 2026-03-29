namespace NovaFit.Domain.Entities;

public class PrestamoCasillero
{
    public Guid Id { get; set; }
    public Guid CasilleroId { get; set; }
    public Guid ClienteId { get; set; }
    public string DocumentoRetenido { get; set; } = string.Empty;
    public DateTime FechaHoraPrestamo { get; set; }
    public DateTime? FechaHoraDevolucion { get; set; }
    public string? Observacion { get; set; }
    public DateTime CreadoEn { get; set; }

    public Casillero Casillero { get; set; } = null!;
    public Cliente Cliente { get; set; } = null!;

    public bool EstaActivo() => FechaHoraDevolucion == null;
}
