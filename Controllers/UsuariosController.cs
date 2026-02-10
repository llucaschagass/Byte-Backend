using Byte_Backend.DTOs;
using Byte_Backend.Entidades;
using Byte_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Byte_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
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

        var usuario = new Usuario
        {
            FuncionarioId = dto.FuncionarioId,
            Login = dto.Login,
            SenhaHash = dto.SenhaHash
        };

        var (success, message) = await usuarioService.CreateUsuario(usuario);

        if (!success)
        {
            return BadRequest(new { message });
        }

        return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
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

        var usuario = new Usuario
        {
            FuncionarioId = dto.FuncionarioId,
            Login = dto.Login,
            SenhaHash = dto.SenhaHash
        };

        var (success, message) = await usuarioService.UpdateUsuario(id, usuario);

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
