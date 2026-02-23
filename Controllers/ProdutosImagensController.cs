using Byte_Backend.DTOs;
using Byte_Backend.Entidades;
using Byte_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Byte_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProdutosImagensController : ControllerBase
{
    private readonly ProdutoImagemService produtoImagemService;

    public ProdutosImagensController(ProdutoImagemService produtoImagemService)
    {
        this.produtoImagemService = produtoImagemService;
    }

    /// <summary>
    /// Retorna todas as imagens de um produto específico
    /// </summary>
    [HttpGet("produto/{produtoId}")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<ProdutoImagem>>> GetByProduto(int produtoId)
    {
        var imagens = await produtoImagemService.GetAllImagensByProduto(produtoId);
        return Ok(imagens);
    }

    /// <summary>
    /// Retorna uma imagem específica por ID
    /// </summary>
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<ProdutoImagem>> GetById(int id)
    {
        var imagem = await produtoImagemService.GetImagemById(id);

        if (imagem == null)
        {
            return NotFound(new { message = "Imagem não encontrada." });
        }

        return Ok(imagem);
    }

    /// <summary>
    /// Cadastra uma nova imagem para um produto (base64)
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ProdutoImagem>> Create([FromBody] CreateProdutoImagemDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var imagem = new ProdutoImagem
        {
            ProdutoId = dto.ProdutoId,
            Conteudo = dto.Conteudo
        };

        var (success, message) = await produtoImagemService.CreateImagem(imagem);

        if (!success)
        {
            return BadRequest(new { message });
        }

        return CreatedAtAction(nameof(GetById), new { id = imagem.ProdutoImagemId }, imagem);
    }

    /// <summary>
    /// Exclui uma imagem
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var (success, message) = await produtoImagemService.DeleteImagem(id);

        if (!success)
        {
            return NotFound(new { message });
        }

        return Ok(new { message });
    }
}
