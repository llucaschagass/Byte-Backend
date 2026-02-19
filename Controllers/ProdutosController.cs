using Byte_Backend.DTOs;
using Byte_Backend.Entidades;
using Byte_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Byte_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProdutosController : ControllerBase
{
    private readonly ProdutoService produtoService;

    public ProdutosController(ProdutoService produtoService)
    {
        this.produtoService = produtoService;
    }

    /// <summary>
    /// Retorna todos os produtos cadastrados
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<Produto>>> GetAll()
    {
        var produtos = await produtoService.GetAllProdutos();
        return Ok(produtos);
    }

    /// <summary>
    /// Retorna produtos filtrados por CategoriaId
    /// </summary>
    [HttpGet("categoria/{categoriaId}")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<Produto>>> GetByCategoria(int categoriaId)
    {
        var produtos = await produtoService.GetProdutosByCategoria(categoriaId);
        return Ok(produtos);
    }

    /// <summary>
    /// Retorna um produto específico por ID
    /// </summary>
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<Produto>> GetById(int id)
    {
        var produto = await produtoService.GetProdutoById(id);
        
        if (produto == null)
        {
            return NotFound(new { message = "Produto não encontrado." });
        }

        return Ok(produto);
    }

    /// <summary>
    /// Cria um novo produto
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Produto>> Create([FromBody] CreateProdutoDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var produto = new Produto
        {
            Nome = dto.Nome,
            Preco = dto.Preco,
            Descricao = dto.Descricao,
            CategoriaId = dto.CategoriaId,
            Ativo = dto.Ativo
        };

        var (success, message) = await produtoService.CreateProduto(produto);

        if (!success)
        {
            return BadRequest(new { message });
        }

        return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
    }

    /// <summary>
    /// Atualiza um produto existente
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateProdutoDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var produto = new Produto
        {
            Id = id,
            Nome = dto.Nome,
            Preco = dto.Preco,
            Descricao = dto.Descricao,
            CategoriaId = dto.CategoriaId,
            Ativo = dto.Ativo
        };

        var (success, message) = await produtoService.UpdateProduto(id, produto);

        if (!success)
        {
            return NotFound(new { message });
        }

        return Ok(new { message });
    }

    /// <summary>
    /// Exclui um produto
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var (success, message) = await produtoService.DeleteProduto(id);

        if (!success)
        {
            return NotFound(new { message });
        }

        return Ok(new { message });
    }
}
