namespace NovaFit.Application.DTOs;

public class SuscripcionDto
{
    public Guid Id { get; set; }
    public Guid ClienteId { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaVencimiento { get; set; }
    public string Estado { get; set; } = string.Empty;
        public bool EstaVigente { get; set; }
}

public class CreateSuscripcionDto
{
    public Guid ClienteId { get; set; }
    public string Tipo { get; set; } = "mensual";
    public decimal Precio { get; set; }
    }
