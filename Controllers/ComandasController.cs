using Byte_Backend.DTOs;
using Byte_Backend.Entidades;
using Byte_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Byte_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ComandasController : ControllerBase
{
    private readonly ComandaService comandaService;

    public ComandasController(ComandaService comandaService)
    {
        this.comandaService = comandaService;
    }

    /// <summary>
    /// Retorna todas as comandas cadastradas
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Comanda>>> GetAll()
    {
        var comandas = await comandaService.GetAllComandas();
        return Ok(comandas);
    }

    /// <summary>
    /// Retorna uma comanda específica por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<Comanda>> GetById(int id)
    {
        var comanda = await comandaService.GetComandaById(id);
        
        if (comanda == null)
        {
            return NotFound(new { message = "Comanda não encontrada." });
        }

        return Ok(comanda);
    }

    /// <summary>
    /// Retorna todas as comandas de um cliente específico
    /// </summary>
    [HttpGet("cliente/{clienteId}")]
    public async Task<ActionResult<IEnumerable<Comanda>>> GetByClienteId(int clienteId)
    {
        var comandas = await comandaService.GetComandasByClienteId(clienteId);
        return Ok(comandas);
    }

    /// <summary>
    /// Retorna todas as comandas de um cartão específico
    /// </summary>
    [HttpGet("cartao/{cartaoId}")]
    public async Task<ActionResult<IEnumerable<Comanda>>> GetByCartaoId(int cartaoId)
    {
        var comandas = await comandaService.GetComandasByCartaoId(cartaoId);
        return Ok(comandas);
    }

    /// <summary>
    /// Cria uma nova comanda
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Comanda>> Create([FromBody] CreateComandaDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var comanda = new Comanda
        {
            CartaoId = dto.CartaoId,
            ClienteId = dto.ClienteId,
            Status = dto.Status,
            AbertaEm = dto.AbertaEm,
            FechadaEm = dto.FechadaEm
        };

        var (success, message) = await comandaService.CreateComanda(comanda);

        if (!success)
        {
            return BadRequest(new { message });
        }

        return CreatedAtAction(nameof(GetById), new { id = comanda.Id }, comanda);
    }

    /// <summary>
    /// Atualiza uma comanda existente
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateComandaDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var comanda = new Comanda
        {
            CartaoId = dto.CartaoId,
            ClienteId = dto.ClienteId,
            Status = dto.Status,
            AbertaEm = dto.AbertaEm,
            FechadaEm = dto.FechadaEm
        };

        var (success, message) = await comandaService.UpdateComanda(id, comanda);

        if (!success)
        {
            return NotFound(new { message });
        }

        return Ok(new { message });
    }

    /// <summary>
    /// Fecha uma comanda aberta
    /// </summary>
    [HttpPost("{id}/fechar")]
    public async Task<ActionResult> FecharComanda(int id)
    {
        var (success, message) = await comandaService.FecharComanda(id);

        if (!success)
        {
            return BadRequest(new { message });
        }

        return Ok(new { message });
    }

    /// <summary>
    /// Exclui uma comanda
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var (success, message) = await comandaService.DeleteComanda(id);

        if (!success)
        {
            return NotFound(new { message });
        }

        return Ok(new { message });
    }
}
