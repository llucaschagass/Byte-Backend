using Byte_Backend.DTOs;
using Byte_Backend.Entidades;
using Byte_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Byte_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SolicitacoesAtendimentoController : ControllerBase
{
    private readonly SolicitacaoAtendimentoService solicitacaoService;

    public SolicitacoesAtendimentoController(SolicitacaoAtendimentoService solicitacaoService)
    {
        this.solicitacaoService = solicitacaoService;
    }

    /// <summary>
    /// Retorna todas as solicitações de atendimento
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SolicitacaoAtendimento>>> GetAll()
    {
        var solicitacoes = await solicitacaoService.GetAll();
        return Ok(solicitacoes);
    }

    /// <summary>
    /// Retorna uma solicitação de atendimento por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<SolicitacaoAtendimento>> GetById(int id)
    {
        var solicitacao = await solicitacaoService.GetById(id);

        if (solicitacao == null)
        {
            return NotFound(new { message = "Solicitação de atendimento não encontrada." });
        }

        return Ok(solicitacao);
    }

    /// <summary>
    /// Retorna todas as solicitações de atendimento pendentes (não atendidas)
    /// </summary>
    [HttpGet("pendentes")]
    public async Task<ActionResult<IEnumerable<SolicitacaoAtendimento>>> GetPendentes()
    {
        var pendentes = await solicitacaoService.GetPendentes();
        return Ok(pendentes);
    }

    /// <summary>
    /// Cria uma solicitação de atendimento (chamar garçom). Não requer autenticação.
    /// </summary>
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<SolicitacaoAtendimento>> Create([FromBody] CreateSolicitacaoAtendimentoDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var (success, message, solicitacao) = await solicitacaoService.CriarSolicitacao(dto.NumeroMesa);

        if (!success)
        {
            return BadRequest(new { message });
        }

        return CreatedAtAction(nameof(GetById), new { id = solicitacao!.Id }, solicitacao);
    }

    /// <summary>
    /// Marca uma solicitação de atendimento como atendida, registrando o garçom responsável
    /// </summary>
    [HttpPatch("{id}/atender")]
    public async Task<ActionResult> Atender(int id, [FromBody] AtenderSolicitacaoDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var (success, message) = await solicitacaoService.AtenderSolicitacao(id, dto.UsuarioId);

        if (!success)
        {
            return BadRequest(new { message });
        }

        return Ok(new { message });
    }
}
