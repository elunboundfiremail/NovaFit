using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NovaFit.Application.DTOs;
using NovaFit.Application.Interfaces;

namespace NovaFit.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MembresiasController : ControllerBase
{
    private readonly IMembresiaService _membresiaService;

    public MembresiasController(IMembresiaService membresiaService)
    {
        _membresiaService = membresiaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MembresiaDto>>> ObtenerTodas()
    {
        var membresias = await _membresiaService.ObtenerTodas();
        return Ok(membresias);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MembresiaDto>> ObtenerPorId(Guid id)
    {
        var membresia = await _membresiaService.ObtenerPorId(id);
        if (membresia is null)
            return NotFound("Membresia no encontrada");

        return Ok(membresia);
    }

    [HttpGet("cliente/{clienteId}/activa")]
    public async Task<ActionResult<MembresiaDto>> ObtenerActivaPorCliente(Guid clienteId)
    {
        var membresia = await _membresiaService.ObtenerActivaPorCliente(clienteId);
        if (membresia is null)
            return NotFound("Cliente sin membresia activa");

        return Ok(membresia);
    }

    [HttpPost]
    public async Task<ActionResult<MembresiaDto>> Crear([FromBody] CreateMembresiaDto dto)
    {
        try
        {
            var membresia = await _membresiaService.Crear(dto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = membresia.Id }, membresia);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}/cancelar")]
    public async Task<ActionResult> CancelarMembresia(Guid id)
    {
        var cancelada = await _membresiaService.CancelarMembresia(id);
        if (!cancelada)
            return NotFound("Membresia no encontrada");

        return NoContent();
    }
}
