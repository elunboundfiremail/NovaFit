namespace NovaFit.Application.DTOs;

public class IngresoDto
{
    public Guid Id { get; set; }
    public Guid ClienteId { get; set; }
    public Guid? SuscripcionId { get; set; }
    public DateTime FechaIngreso { get; set; }
    public TimeSpan HoraIngreso { get; set; }
    public TimeSpan? HoraSalida { get; set; }
    public bool SalidaRegistrada { get; set; }
    public int? DuracionMinutos { get; set; }
    public string NombreCliente { get; set; } = string.Empty;
    public int CiCliente { get; set; }
}

public class ValidarIngresoDto
{
    public int Ci { get; set; }
}
