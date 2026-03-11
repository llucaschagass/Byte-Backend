using Byte_Backend.DTOs;
using Byte_Backend.Entidades;
using Byte_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Byte_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class IngredientesReceitasController : ControllerBase
{
    private readonly IngredienteReceitaService ingredienteService;

    public IngredientesReceitasController(IngredienteReceitaService ingredienteService)
    {
        this.ingredienteService = ingredienteService;
    }

    /// <summary>
    /// Retorna ingredientes de uma receita
    /// </summary>
    [HttpGet("receita/{receitaId}")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<IngredienteReceita>>> GetByReceita(int receitaId)
    {
        var ingredientes = await ingredienteService.GetByReceitaId(receitaId);
        return Ok(ingredientes);
    }

    /// <summary>
    /// Retorna um ingrediente por ID
    /// </summary>
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<IngredienteReceita>> GetById(int id)
    {
        var ingrediente = await ingredienteService.GetById(id);

        if (ingrediente == null)
        {
            return NotFound(new { message = "Ingrediente não encontrado." });
        }

        return Ok(ingrediente);
    }

    /// <summary>
    /// Adiciona um ingrediente a uma receita
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<IngredienteReceita>> Create([FromBody] CreateIngredienteReceitaDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var ingrediente = new IngredienteReceita
        {
            ReceitaId = dto.ReceitaId,
            Nome = dto.Nome,
            Quantidade = dto.Quantidade,
            Observacao = dto.Observacao
        };

        var (success, message) = await ingredienteService.Create(ingrediente);

        if (!success)
        {
            return BadRequest(new { message });
        }

        return CreatedAtAction(nameof(GetById), new { id = ingrediente.Id }, ingrediente);
    }

    /// <summary>
    /// Atualiza um ingrediente
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateIngredienteReceitaDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var ingrediente = new IngredienteReceita
        {
            Id = id,
            Nome = dto.Nome,
            Quantidade = dto.Quantidade,
            Observacao = dto.Observacao
        };

        var (success, message) = await ingredienteService.Update(id, ingrediente);

        if (!success)
        {
            return NotFound(new { message });
        }

        return Ok(new { message });
    }

    /// <summary>
    /// Exclui um ingrediente
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var (success, message) = await ingredienteService.Delete(id);

        if (!success)
        {
            return NotFound(new { message });
        }

        return Ok(new { message });
    }
}
