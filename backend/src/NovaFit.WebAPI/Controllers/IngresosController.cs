using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NovaFit.Application.DTOs;
using NovaFit.Application.Interfaces;

namespace NovaFit.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class IngresosController : ControllerBase
{
    private readonly IIngresoService _ingresoService;

    public IngresosController(IIngresoService ingresoService)
    {
        _ingresoService = ingresoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<IngresoDto>>> ObtenerTodos()
    {
        var ingresos = await _ingresoService.ObtenerTodos();
        return Ok(ingresos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IngresoDto>> ObtenerPorId(Guid id)
    {
        var ingreso = await _ingresoService.ObtenerPorId(id);
        if (ingreso is null)
            return NotFound("Ingreso no encontrado");

        return Ok(ingreso);
    }

    [HttpGet("cliente/{clienteId}")]
    public async Task<ActionResult<IEnumerable<IngresoDto>>> ObtenerPorCliente(Guid clienteId)
    {
        var ingresos = await _ingresoService.ObtenerPorCliente(clienteId);
        return Ok(ingresos);
    }

    [HttpGet("rechazados")]
    public async Task<ActionResult<IEnumerable<IngresoDto>>> ObtenerRechazados()
    {
        var ingresos = await _ingresoService.ObtenerRechazados();
        return Ok(ingresos);
    }

    [HttpPost("registrar")]
    public async Task<ActionResult<IngresoDto>> Registrar([FromBody] ValidarIngresoDto dto)
    {
        try
        {
            var ingreso = await _ingresoService.RegistrarIngreso(dto.Ci);
            return Ok(ingreso);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("{id}/salida")]
    public async Task<ActionResult<IngresoDto>> RegistrarSalida(Guid id)
    {
        try
        {
            var ingreso = await _ingresoService.RegistrarSalida(id);
            if (ingreso is null)
                return NotFound("Ingreso no encontrado");

            return Ok(ingreso);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
