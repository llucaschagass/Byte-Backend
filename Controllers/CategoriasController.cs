using Byte_Backend.DTOs;
using Byte_Backend.Entidades;
using Byte_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Byte_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CategoriasController : ControllerBase
{
    private readonly CategoriaService categoriaService;

    public CategoriasController(CategoriaService categoriaService)
    {
        this.categoriaService = categoriaService;
    }

    /// <summary>
    /// Retorna todas as categorias cadastradas
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetAll()
    {
        var categorias = await categoriaService.GetAllCategorias();
        return Ok(categorias);
    }

    /// <summary>
    /// Retorna uma categoria específica por ID
    /// </summary>
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<Categoria>> GetById(int id)
    {
        var categoria = await categoriaService.GetCategoriaById(id);
        
        if (categoria == null)
        {
            return NotFound(new { message = "Categoria não encontrada." });
        }

        return Ok(categoria);
    }

    /// <summary>
    /// Cria uma nova categoria
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Categoria>> Create([FromBody] CreateCategoriaDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var categoria = new Categoria
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao
        };

        var (success, message) = await categoriaService.CreateCategoria(categoria);

        if (!success)
        {
            return BadRequest(new { message });
        }

        return CreatedAtAction(nameof(GetById), new { id = categoria.CategoriaId }, categoria);
    }

    /// <summary>
    /// Atualiza uma categoria existente
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateCategoriaDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var categoria = new Categoria
        {
            CategoriaId = id,
            Nome = dto.Nome,
            Descricao = dto.Descricao
        };

        var (success, message) = await categoriaService.UpdateCategoria(id, categoria);

        if (!success)
        {
            return NotFound(new { message });
        }

        return Ok(new { message });
    }

    /// <summary>
    /// Exclui uma categoria
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var (success, message) = await categoriaService.DeleteCategoria(id);

        if (!success)
        {
            return NotFound(new { message });
        }

        return Ok(new { message });
    }
}
