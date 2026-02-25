using Byte_Backend.DTOs;
using Byte_Backend.Entidades;
using Byte_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Byte_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsuariosPermissoesController : ControllerBase
{
    private readonly UsuarioPermissaoService usuarioPermissaoService;

    public UsuariosPermissoesController(UsuarioPermissaoService usuarioPermissaoService)
    {
        this.usuarioPermissaoService = usuarioPermissaoService;
    }

    /// <summary>
    /// Retorna todas as permissões de usuários
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsuarioPermissao>>> GetAll()
    {
        var permissoes = await usuarioPermissaoService.GetAll();
        return Ok(permissoes);
    }

    /// <summary>
    /// Retorna a permissão de um usuário pelo ID da permissão
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<UsuarioPermissao>> GetById(int id)
    {
        var permissao = await usuarioPermissaoService.GetById(id);

        if (permissao == null)
            return NotFound(new { message = "Permissão não encontrada." });

        return Ok(permissao);
    }

    /// <summary>
    /// Retorna a permissão de um usuário pelo ID do usuário
    /// </summary>
    [HttpGet("usuario/{usuarioId}")]
    public async Task<ActionResult<UsuarioPermissao>> GetByUsuarioId(int usuarioId)
    {
        var permissao = await usuarioPermissaoService.GetByUsuarioId(usuarioId);

        if (permissao == null)
            return NotFound(new { message = "Nenhuma permissão encontrada para este usuário." });

        return Ok(permissao);
    }

    /// <summary>
    /// Cadastra permissões para um usuário
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<UsuarioPermissao>> Create([FromBody] CreateUsuarioPermissaoDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var (success, message, permissao) = await usuarioPermissaoService.Create(
            dto.UsuarioId, dto.Garcom, dto.Cozinha, dto.Gerencia, dto.Principal);

        if (!success)
            return BadRequest(new { message });

        return CreatedAtAction(nameof(GetById), new { id = permissao!.Id }, permissao);
    }

    /// <summary>
    /// Atualiza as permissões de um usuário
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateUsuarioPermissaoDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var (success, message) = await usuarioPermissaoService.Update(
            id, dto.Garcom, dto.Cozinha, dto.Gerencia, dto.Principal);

        if (!success)
            return BadRequest(new { message });

        return Ok(new { message });
    }

    /// <summary>
    /// Remove as permissões de um usuário
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var (success, message) = await usuarioPermissaoService.Delete(id);

        if (!success)
            return NotFound(new { message });

        return Ok(new { message });
    }
}
