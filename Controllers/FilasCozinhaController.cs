using Byte_Backend.DTOs;
using Byte_Backend.Entidades;
using Byte_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Byte_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FilasCozinhaController : ControllerBase
{
    private readonly FilaCozinhaService filaCozinhaService;

    public FilasCozinhaController(FilaCozinhaService filaCozinhaService)
    {
        this.filaCozinhaService = filaCozinhaService;
    }

    /// <summary>
    /// Retorna todos os itens da fila da cozinha
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FilaCozinha>>> GetAll()
    {
        var filas = await filaCozinhaService.GetAllFilasCozinha();
        return Ok(filas);
    }

    /// <summary>
    /// Retorna um item específico da fila da cozinha por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<FilaCozinha>> GetById(int id)
    {
        var fila = await filaCozinhaService.GetFilaCozinhaById(id);
        
        if (fila == null)
        {
            return NotFound(new { message = "Item da fila da cozinha não encontrado." });
        }

        return Ok(fila);
    }

    /// <summary>
    /// Retorna todos os itens da fila da cozinha com um status específico
    /// </summary>
    [HttpGet("status/{status}")]
    public async Task<ActionResult<IEnumerable<FilaCozinha>>> GetByStatus(string status)
    {
        var filas = await filaCozinhaService.GetFilasCozinhaByStatus(status);
        return Ok(filas);
    }

    /// <summary>
    /// Retorna todos os itens da fila da cozinha de um item de comanda específico
    /// </summary>
    [HttpGet("item/{itemComandaId}")]
    public async Task<ActionResult<IEnumerable<FilaCozinha>>> GetByItemComandaId(int itemComandaId)
    {
        var filas = await filaCozinhaService.GetFilasCozinhaByItemComandaId(itemComandaId);
        return Ok(filas);
    }

    /// <summary>
    /// Cria um novo item na fila da cozinha
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<FilaCozinha>> Create([FromBody] CreateFilaCozinhaDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var filaCozinha = new FilaCozinha
        {
            ItemComandaId = dto.ItemComandaId,
            StatusPreparo = dto.StatusPreparo
        };

        var (success, message) = await filaCozinhaService.CreateFilaCozinha(filaCozinha);

        if (!success)
        {
            return BadRequest(new { message });
        }

        return CreatedAtAction(nameof(GetById), new { id = filaCozinha.Id }, filaCozinha);
    }

    /// <summary>
    /// Atualiza um item existente da fila da cozinha
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateFilaCozinhaDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var filaCozinha = new FilaCozinha
        {
            ItemComandaId = dto.ItemComandaId,
            StatusPreparo = dto.StatusPreparo
        };

        var (success, message) = await filaCozinhaService.UpdateFilaCozinha(id, filaCozinha);

        if (!success)
        {
            return NotFound(new { message });
        }

        return Ok(new { message });
    }

    /// <summary>
    /// Atualiza apenas o status de preparo de um item
    /// </summary>
    [HttpPatch("{id}/status")]
    public async Task<ActionResult> UpdateStatus(int id, [FromBody] string novoStatus)
    {
        var (success, message) = await filaCozinhaService.AtualizarStatusPreparo(id, novoStatus);

        if (!success)
        {
            return BadRequest(new { message });
        }

        return Ok(new { message });
    }

    /// <summary>
    /// Exclui um item da fila da cozinha
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var (success, message) = await filaCozinhaService.DeleteFilaCozinha(id);

        if (!success)
        {
            return NotFound(new { message });
        }

        return Ok(new { message });
    }
}
