using Byte_Backend.DTOs;
using Byte_Backend.Entidades;
using Byte_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Byte_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsuariosController : ControllerBase
{
    private readonly UsuarioService usuarioService;

    public UsuariosController(UsuarioService usuarioService)
    {
        this.usuarioService = usuarioService;
    }

    /// <summary>
    /// Retorna todos os usuários cadastrados
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Usuario>>> GetAll()
    {
        var usuarios = await usuarioService.GetAllUsuarios();
        return Ok(usuarios);
    }

    /// <summary>
    /// Retorna um usuário específico por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<Usuario>> GetById(int id)
    {
        var usuario = await usuarioService.GetUsuarioById(id);
        
        if (usuario == null)
        {
            return NotFound(new { message = "Usuário não encontrado." });
        }

        return Ok(usuario);
    }

    /// <summary>
    /// Cria um novo usuário
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Usuario>> Create([FromBody] CreateUsuarioDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var (success, message) = await usuarioService.CreateUsuarioComSenha(dto.Login, dto.FuncionarioId, dto.Senha);

        if (!success)
        {
            return BadRequest(new { message });
        }

        // Buscar o usuário criado para retornar
        var usuarios = await usuarioService.GetAllUsuarios();
        var usuarioCriado = usuarios.FirstOrDefault(u => u.Login == dto.Login);

        return CreatedAtAction(nameof(GetById), new { id = usuarioCriado?.Id }, usuarioCriado);
    }

    /// <summary>
    /// Atualiza um usuário existente
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateUsuarioDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var (success, message) = await usuarioService.UpdateUsuarioComSenha(id, dto.Login, dto.FuncionarioId, dto.Senha);

        if (!success)
        {
            return NotFound(new { message });
        }

        return Ok(new { message });
    }

    /// <summary>
    /// Exclui um usuário
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var (success, message) = await usuarioService.DeleteUsuario(id);

        if (!success)
        {
            return NotFound(new { message });
        }

        return Ok(new { message });
    }
}
