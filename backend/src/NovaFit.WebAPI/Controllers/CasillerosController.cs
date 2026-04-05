using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NovaFit.Application.DTOs;
using NovaFit.Application.Interfaces;

namespace NovaFit.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CasillerosController : ControllerBase
{
    private readonly ICasilleroService _casilleroService;

    public CasillerosController(ICasilleroService casilleroService)
    {
        _casilleroService = casilleroService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CasilleroDto>>> ObtenerTodos()
    {
        var casilleros = await _casilleroService.ObtenerTodos();
        return Ok(casilleros);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CasilleroDto>> ObtenerPorId(Guid id)
    {
        var casillero = await _casilleroService.ObtenerPorId(id);
        if (casillero is null)
            return NotFound("Casillero no encontrado");

        return Ok(casillero);
    }

    [HttpGet("disponibles")]
    public async Task<ActionResult<IEnumerable<CasilleroDto>>> ObtenerDisponibles()
    {
        var casilleros = await _casilleroService.ObtenerDisponibles();
        return Ok(casilleros);
    }

    [HttpPost]
    public async Task<ActionResult<CasilleroDto>> Crear([FromBody] CreateCasilleroDto dto)
    {
        try
        {
            var casillero = await _casilleroService.CrearCasillero(dto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = casillero.Id }, casillero);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CasilleroDto>> Actualizar(Guid id, [FromBody] UpdateCasilleroDto dto)
    {
        try
        {
            var casillero = await _casilleroService.ActualizarCasillero(id, dto);
            if (casillero is null)
                return NotFound("Casillero no encontrado");

            return Ok(casillero);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Eliminar(Guid id)
    {
        try
        {
            var eliminado = await _casilleroService.EliminarCasillero(id);
            if (!eliminado)
                return NotFound("Casillero no encontrado");

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("prestamos/activos")]
    public async Task<ActionResult<IEnumerable<PrestamoDto>>> ObtenerPrestamosActivos()
    {
        var prestamos = await _casilleroService.ObtenerPrestamosActivos();
        return Ok(prestamos);
    }

    [HttpGet("historial/{casilleroId}")]
    public async Task<ActionResult<IEnumerable<PrestamoDto>>> ObtenerHistorial(Guid casilleroId)
    {
        var historial = await _casilleroService.ObtenerHistorial(casilleroId);
        return Ok(historial);
    }

    [HttpPost("prestar")]
    public async Task<ActionResult<PrestamoDto>> PrestarCasillero([FromBody] PrestarCasilleroDto dto)
    {
        try
        {
            var prestamo = await _casilleroService.PrestarCasillero(dto);
            return CreatedAtAction(nameof(ObtenerHistorial), new { casilleroId = prestamo.CasilleroId }, prestamo);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("devolver/{prestamoId}")]
    public async Task<ActionResult<PrestamoDto>> DevolverCasillero(Guid prestamoId)
    {
        try
        {
            var prestamo = await _casilleroService.DevolverCasillero(prestamoId);
            return Ok(prestamo);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
