using Byte_Backend.DTOs;
using Byte_Backend.Entidades;
using Byte_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Byte_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OpcoesProdutosController : ControllerBase
{
    private readonly OpcaoProdutoService opcaoService;

    public OpcoesProdutosController(OpcaoProdutoService opcaoService)
    {
        this.opcaoService = opcaoService;
    }

    /// <summary>
    /// Retorna todas as opções cadastradas
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OpcaoProduto>>> GetAll()
    {
        var opcoes = await opcaoService.GetAllOpcoes();
        return Ok(opcoes);
    }

    /// <summary>
    /// Retorna opções filtradas por ProdutoId
    /// </summary>
    [HttpGet("produto/{produtoId}")]
    public async Task<ActionResult<IEnumerable<OpcaoProduto>>> GetByProduto(int produtoId)
    {
        var opcoes = await opcaoService.GetOpcoesByProduto(produtoId);
        return Ok(opcoes);
    }

    /// <summary>
    /// Retorna uma opção específica por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<OpcaoProduto>> GetById(int id)
    {
        var opcao = await opcaoService.GetOpcaoById(id);
        
        if (opcao == null)
        {
            return NotFound(new { message = "Opção não encontrada." });
        }

        return Ok(opcao);
    }

    /// <summary>
    /// Cria uma nova opção para um produto
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<OpcaoProduto>> Create([FromBody] CreateOpcaoProdutoDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var opcao = new OpcaoProduto
        {
            ProdutoId = dto.ProdutoId,
            Nome = dto.Nome,
            PrecoAdicional = dto.PrecoAdicional
        };

        var (success, message) = await opcaoService.CreateOpcao(opcao);

        if (!success)
        {
            return BadRequest(new { message });
        }

        return CreatedAtAction(nameof(GetById), new { id = opcao.Id }, opcao);
    }

    /// <summary>
    /// Atualiza uma opção existente
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateOpcaoProdutoDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var opcao = new OpcaoProduto
        {
            Id = id,
            ProdutoId = dto.ProdutoId,
            Nome = dto.Nome,
            PrecoAdicional = dto.PrecoAdicional
        };

        var (success, message) = await opcaoService.UpdateOpcao(id, opcao);

        if (!success)
        {
            return NotFound(new { message });
        }

        return Ok(new { message });
    }

    /// <summary>
    /// Exclui uma opção
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var (success, message) = await opcaoService.DeleteOpcao(id);

        if (!success)
        {
            return NotFound(new { message });
        }

        return Ok(new { message });
    }
}
