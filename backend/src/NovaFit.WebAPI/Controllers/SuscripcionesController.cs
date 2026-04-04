using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NovaFit.Application.DTOs;
using NovaFit.Application.Interfaces;

namespace NovaFit.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SuscripcionsController : ControllerBase
{
    private readonly ISuscripcionService _SuscripcionService;

    public SuscripcionsController(ISuscripcionService SuscripcionService)
    {
        _SuscripcionService = SuscripcionService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SuscripcionDto>>> ObtenerTodas()
    {
        var Suscripcions = await _SuscripcionService.ObtenerTodas();
        return Ok(Suscripcions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SuscripcionDto>> ObtenerPorId(Guid id)
    {
        var Suscripcion = await _SuscripcionService.ObtenerPorId(id);
        if (Suscripcion is null)
            return NotFound("Suscripcion no encontrada");

        return Ok(Suscripcion);
    }

    [HttpGet("cliente/{clienteId}/activa")]
    public async Task<ActionResult<SuscripcionDto>> ObtenerActivaPorCliente(Guid clienteId)
    {
        var Suscripcion = await _SuscripcionService.ObtenerActivaPorCliente(clienteId);
        if (Suscripcion is null)
            return NotFound("Cliente sin Suscripcion activa");

        return Ok(Suscripcion);
    }

    [HttpPost]
    public async Task<ActionResult<SuscripcionDto>> Crear([FromBody] CreateSuscripcionDto dto)
    {
        try
        {
            var Suscripcion = await _SuscripcionService.Crear(dto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = Suscripcion.Id }, Suscripcion);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}/cancelar")]
    public async Task<ActionResult> CancelarSuscripcion(Guid id)
    {
        var cancelada = await _SuscripcionService.CancelarSuscripcion(id);
        if (!cancelada)
            return NotFound("Suscripcion no encontrada");

        return NoContent();
    }
}
