using Byte_Backend.DTOs;
using Byte_Backend.Entidades;
using Byte_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Byte_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly ClienteService clienteService;

    public ClientesController(ClienteService clienteService)
    {
        this.clienteService = clienteService;
    }

    /// <summary>
    /// Retorna todos os clientes cadastrados
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cliente>>> GetAll()
    {
        var clientes = await clienteService.GetAllClientes();
        return Ok(clientes);
    }

    /// <summary>
    /// Retorna um cliente específico por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<Cliente>> GetById(int id)
    {
        var cliente = await clienteService.GetClienteById(id);
        
        if (cliente == null)
        {
            return NotFound(new { message = "Cliente não encontrado." });
        }

        return Ok(cliente);
    }

    /// <summary>
    /// Cria um novo cliente
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Cliente>> Create([FromBody] CreateClienteDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var cliente = new Cliente
        {
            PessoaId = dto.PessoaId,
            PontosFidelidade = dto.PontosFidelidade
        };

        var (success, message) = await clienteService.CreateCliente(cliente);

        if (!success)
        {
            return BadRequest(new { message });
        }

        return CreatedAtAction(nameof(GetById), new { id = cliente.Id }, cliente);
    }

    /// <summary>
    /// Atualiza um cliente existente
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateClienteDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var cliente = new Cliente
        {
            PessoaId = dto.PessoaId,
            PontosFidelidade = dto.PontosFidelidade
        };

        var (success, message) = await clienteService.UpdateCliente(id, cliente);

        if (!success)
        {
            return NotFound(new { message });
        }

        return Ok(new { message });
    }

    /// <summary>
    /// Exclui um cliente
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var (success, message) = await clienteService.DeleteCliente(id);

        if (!success)
        {
            return NotFound(new { message });
        }

        return Ok(new { message });
    }
}
