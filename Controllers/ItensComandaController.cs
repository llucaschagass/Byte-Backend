using Byte_Backend.DTOs;
using Byte_Backend.Entidades;
using Byte_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Byte_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ItensComandaController : ControllerBase
{
    private readonly ItemComandaService itemComandaService;

    public ItensComandaController(ItemComandaService itemComandaService)
    {
        this.itemComandaService = itemComandaService;
    }

    /// <summary>
    /// Retorna todos os itens de comanda cadastrados
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ItemComanda>>> GetAll()
    {
        var itens = await itemComandaService.GetAllItensComanda();
        return Ok(itens);
    }

    /// <summary>
    /// Retorna um item de comanda específico por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ItemComanda>> GetById(int id)
    {
        var item = await itemComandaService.GetItemComandaById(id);
        
        if (item == null)
        {
            return NotFound(new { message = "Item da comanda não encontrado." });
        }

        return Ok(item);
    }

    /// <summary>
    /// Retorna todos os itens de uma comanda específica
    /// </summary>
    [HttpGet("comanda/{comandaId}")]
    public async Task<ActionResult<IEnumerable<ItemComanda>>> GetByComandaId(int comandaId)
    {
        var itens = await itemComandaService.GetItensByComandaId(comandaId);
        return Ok(itens);
    }

    /// <summary>
    /// Adiciona um novo item à comanda
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ItemComanda>> Create([FromBody] CreateItemComandaDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var item = new ItemComanda
        {
            ComandaId = dto.ComandaId,
            ProdutoId = dto.ProdutoId,
            OpcaoProdutoId = dto.OpcaoProdutoId,
            Quantidade = dto.Quantidade,
            PrecoUnitario = dto.PrecoUnitario,
            Observacao = dto.Observacao
        };

        var (success, message) = await itemComandaService.CreateItemComanda(item);

        if (!success)
        {
            return BadRequest(new { message });
        }

        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    /// <summary>
    /// Atualiza um item de comanda existente
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateItemComandaDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var item = new ItemComanda
        {
            ComandaId = dto.ComandaId,
            ProdutoId = dto.ProdutoId,
            OpcaoProdutoId = dto.OpcaoProdutoId,
            Quantidade = dto.Quantidade,
            PrecoUnitario = dto.PrecoUnitario,
            Observacao = dto.Observacao
        };

        var (success, message) = await itemComandaService.UpdateItemComanda(id, item);

        if (!success)
        {
            return NotFound(new { message });
        }

        return Ok(new { message });
    }

    /// <summary>
    /// Remove um item da comanda
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var (success, message) = await itemComandaService.DeleteItemComanda(id);

        if (!success)
        {
            return NotFound(new { message });
        }

        return Ok(new { message });
    }
}
