using Byte_Backend.DTOs;
using Byte_Backend.Entidades;
using Byte_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Byte_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FuncionariosController : ControllerBase
{
    private readonly FuncionarioService funcionarioService;

    public FuncionariosController(FuncionarioService funcionarioService)
    {
        this.funcionarioService = funcionarioService;
    }

    /// <summary>
    /// Retorna todos os funcionários cadastrados
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Funcionario>>> GetAll()
    {
        var funcionarios = await funcionarioService.GetAllFuncionarios();
        return Ok(funcionarios);
    }

    /// <summary>
    /// Retorna um funcionário específico por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<Funcionario>> GetById(int id)
    {
        var funcionario = await funcionarioService.GetFuncionarioById(id);
        
        if (funcionario == null)
        {
            return NotFound(new { message = "Funcionário não encontrado." });
        }

        return Ok(funcionario);
    }

    /// <summary>
    /// Cria um novo funcionário
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Funcionario>> Create([FromBody] CreateFuncionarioDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var funcionario = new Funcionario
        {
            PessoaId = dto.PessoaId,
            CargoId = dto.CargoId
        };

        var (success, message) = await funcionarioService.CreateFuncionario(funcionario);

        if (!success)
        {
            return BadRequest(new { message });
        }

        return CreatedAtAction(nameof(GetById), new { id = funcionario.Id }, funcionario);
    }

    /// <summary>
    /// Atualiza um funcionário existente
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateFuncionarioDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var funcionario = new Funcionario
        {
            PessoaId = dto.PessoaId,
            CargoId = dto.CargoId,
            Ativo = dto.Ativo
        };

        var (success, message) = await funcionarioService.UpdateFuncionario(id, funcionario);

        if (!success)
        {
            return NotFound(new { message });
        }

        return Ok(new { message });
    }

    /// <summary>
    /// Exclui um funcionário
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var (success, message) = await funcionarioService.DeleteFuncionario(id);

        if (!success)
        {
            return NotFound(new { message });
        }

        return Ok(new { message });
    }
}
