namespace NovaFit.Domain.Entities;

public class Casillero
{
    public Guid Id { get; set; }
    public int Numero { get; set; }
    public bool Disponible { get; set; } = true;
    public bool Activo { get; set; } = true;
    public DateTime CreadoEn { get; set; }

    public ICollection<PrestamoCasillero> Prestamos { get; set; } = new List<PrestamoCasillero>();
}
