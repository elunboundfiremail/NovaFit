using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NovaFit.Application.DTOs;
using NovaFit.Application.Interfaces;

namespace NovaFit.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ClientesController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClientesController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClienteDto>>> ObtenerTodos()
    {
        var clientes = await _clienteService.ObtenerTodos();
        return Ok(clientes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClienteDto>> ObtenerPorId(Guid id)
    {
        var cliente = await _clienteService.ObtenerPorId(id);
        if (cliente == null)
            return NotFound("Cliente no encontrado");
        
        return Ok(cliente);
    }

    [HttpGet("ci/{ci}")]
    public async Task<ActionResult<ClienteDto>> ObtenerPorCi(int ci)
    {
        var cliente = await _clienteService.ObtenerPorCi(ci);
        if (cliente == null)
            return NotFound("Cliente no encontrado");
        
        return Ok(cliente);
    }

    [HttpPost]
    public async Task<ActionResult<ClienteDto>> Crear([FromBody] CreateClienteDto dto)
    {
        try
        {
            var cliente = await _clienteService.Crear(dto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = cliente.Id }, cliente);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ClienteDto>> Actualizar(Guid id, [FromBody] UpdateClienteDto dto)
    {
        var cliente = await _clienteService.Actualizar(id, dto);
        if (cliente == null)
            return NotFound("Cliente no encontrado");
        
        return Ok(cliente);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Eliminar(Guid id)
    {
        var eliminado = await _clienteService.Eliminar(id);
        if (!eliminado)
            return NotFound("Cliente no encontrado");
        
        return NoContent();
    }
}
