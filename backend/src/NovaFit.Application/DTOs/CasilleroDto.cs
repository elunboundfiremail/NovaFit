namespace NovaFit.Application.DTOs;

public class CasilleroDto
{
    public Guid Id { get; set; }
    public int Numero { get; set; }
    public bool Disponible { get; set; }
    public bool Activo { get; set; }
}

public class PrestamoDto
{
    public Guid Id { get; set; }
    public Guid CasilleroId { get; set; }
    public Guid ClienteId { get; set; }
    public int NumeroCasillero { get; set; }
    public string NombreCliente { get; set; } = string.Empty;
    public string DocumentoRetenido { get; set; } = string.Empty;
    public DateTime FechaHoraPrestamo { get; set; }
    public DateTime? FechaHoraDevolucion { get; set; }
    public bool EstaActivo { get; set; }
}

public class PrestarCasilleroDto
{
    public Guid CasilleroId { get; set; }
    public Guid ClienteId { get; set; }
    public string DocumentoRetenido { get; set; } = string.Empty;
}
