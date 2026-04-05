namespace NovaFit.Application.DTOs;

public class CasilleroDto
{
    public Guid Id { get; set; }
    public int Numero { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public string? Ubicacion { get; set; }
}

public class CreateCasilleroDto
{
    public int Numero { get; set; }
    public string Tipo { get; set; } = "TEMPORAL";
    public string Estado { get; set; } = "DISPONIBLE";
    public string? Ubicacion { get; set; }
}

public class UpdateCasilleroDto
{
    public int Numero { get; set; }
    public string Tipo { get; set; } = "TEMPORAL";
    public string Estado { get; set; } = "DISPONIBLE";
    public string? Ubicacion { get; set; }
}

public class PrestamoDto
{
    public Guid Id { get; set; }
    public Guid? CasilleroId { get; set; }
    public Guid IngresoId { get; set; }
    public int NumeroCasillero { get; set; }
    public string? NumeroTicket { get; set; }
    public string? NumeroLlave { get; set; }
    public int? CiDepositado { get; set; }
    public DateTime FechaPrestamo { get; set; }
    public DateTime? FechaDevolucion { get; set; }
    public bool Devuelto { get; set; }
    public bool EstaActivo { get; set; }
}

public class PrestarCasilleroDto
{
    public Guid CasilleroId { get; set; }
    public Guid IngresoId { get; set; }
    public string? NumeroTicket { get; set; }
    public int? CiDepositado { get; set; }
}
