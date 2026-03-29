namespace NovaFit.Domain.Entities;

public class Ingreso
{
    public Guid Id { get; set; }
    public Guid ClienteId { get; set; }
    public DateTime FechaHoraIngreso { get; set; }
    public bool Permitido { get; set; }
    public string? MotivoAlerta { get; set; }
    public Guid? MembresiaId { get; set; }
    public DateTime CreadoEn { get; set; }

    public Cliente Cliente { get; set; } = null!;
    public Membresia? Membresia { get; set; }
}
