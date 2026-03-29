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
    public DateTime CreadoEn { get; set; }

    public bool EstaVigente()
    {
        var ahora = DateTime.UtcNow.AddHours(-4);
        return Activa && ahora >= FechaInicio && ahora <= FechaFin;
    }
}
