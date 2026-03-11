using Byte_Backend.DTOs;
using Byte_Backend.Entidades;
using Byte_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Byte_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ReceitasController : ControllerBase
{
    private readonly ReceitaService receitaService;

    public ReceitasController(ReceitaService receitaService)
    {
        this.receitaService = receitaService;
    }

    /// <summary>
    /// Retorna todas as receitas cadastradas
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<Receita>>> GetAll()
    {
        var receitas = await receitaService.GetAllReceitas();
        return Ok(receitas);
    }

    /// <summary>
    /// Retorna uma receita específica por ID
    /// </summary>
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<Receita>> GetById(int id)
    {
        var receita = await receitaService.GetReceitaById(id);

        if (receita == null)
        {
            return NotFound(new { message = "Receita não encontrada." });
        }

        return Ok(receita);
    }

    /// <summary>
    /// Retorna a receita de um produto específico
    /// </summary>
    [HttpGet("produto/{produtoId}")]
    [AllowAnonymous]
    public async Task<ActionResult<Receita>> GetByProdutoId(int produtoId)
    {
        var receita = await receitaService.GetReceitaByProdutoId(produtoId);

        if (receita == null)
        {
            return NotFound(new { message = "Nenhuma receita encontrada para este produto." });
        }

        return Ok(receita);
    }

    /// <summary>
    /// Cria uma nova receita
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Receita>> Create([FromBody] CreateReceitaDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var receita = new Receita
        {
            ProdutoId = dto.ProdutoId,
            Descricao = dto.Descricao,
            TempoPreparo = dto.TempoPreparo
        };

        var (success, message) = await receitaService.CreateReceita(receita);

        if (!success)
        {
            return BadRequest(new { message });
        }

        return CreatedAtAction(nameof(GetById), new { id = receita.Id }, receita);
    }

    /// <summary>
    /// Atualiza uma receita existente
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateReceitaDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var receita = new Receita
        {
            Id = id,
            Descricao = dto.Descricao,
            TempoPreparo = dto.TempoPreparo
        };

        var (success, message) = await receitaService.UpdateReceita(id, receita);

        if (!success)
        {
            return NotFound(new { message });
        }

        return Ok(new { message });
    }

    /// <summary>
    /// Exclui uma receita
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var (success, message) = await receitaService.DeleteReceita(id);

        if (!success)
        {
            return NotFound(new { message });
        }

        return Ok(new { message });
    }
}
