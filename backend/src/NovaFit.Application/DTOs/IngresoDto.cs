namespace NovaFit.Application.DTOs;

public class IngresoDto
{
    public Guid Id { get; set; }
    public Guid ClienteId { get; set; }
    public Guid? MembresiaId { get; set; }
    public DateTime FechaHoraIngreso { get; set; }
    public bool Permitido { get; set; }
    public string? MotivoAlerta { get; set; }
    public string NombreCliente { get; set; } = string.Empty;
    public int CiCliente { get; set; }
}

public class ValidarIngresoDto
{
    public int Ci { get; set; }
}
