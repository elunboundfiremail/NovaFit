namespace NovaFit.Application.DTOs;

public class ClienteDto
{
    public Guid Id { get; set; }
    public int Ci { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Telefono { get; set; }
    public DateTime? FechaNacimiento { get; set; }
    public DateTime FechaRegistro { get; set; }
}

public class CreateClienteDto
{
    public int Ci { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Telefono { get; set; }
    public DateTime? FechaNacimiento { get; set; }
}

public class UpdateClienteDto
{
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public string? Email { get; set; }
    public string? Telefono { get; set; }
    public DateTime? FechaNacimiento { get; set; }
}
