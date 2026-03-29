namespace NovaFit.Domain.Entities;

public class Cliente
{
    public Guid Id { get; set; }
    public string Ci { get; set; } = string.Empty;
    public string Nombres { get; set; } = string.Empty;
    public string ApellidoPaterno { get; set; } = string.Empty;
    public string? ApellidoMaterno { get; set; }
    public string TipoCliente { get; set; } = "nuevo";
    public DateTime FechaRegistro { get; set; }
    public bool Activo { get; set; } = true;

    public ICollection<Membresia> Membresias { get; set; } = new List<Membresia>();
    public ICollection<Ingreso> Ingresos { get; set; } = new List<Ingreso>();
    public ICollection<PrestamoCasillero> PrestamosCasilleros { get; set; } = new List<PrestamoCasillero>();
}
