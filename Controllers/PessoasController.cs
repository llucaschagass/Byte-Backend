using Byte_Backend.DTOs;
using Byte_Backend.Entidades;
using Byte_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Byte_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PessoasController : ControllerBase
{
    private readonly PessoaService pessoaService;

    public PessoasController(PessoaService pessoaService)
    {
        this.pessoaService = pessoaService;
    }

    /// <summary>
    /// Retorna todas as pessoas cadastradas
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pessoa>>> GetAll()
    {
        var pessoas = await pessoaService.GetAllPessoas();
        return Ok(pessoas);
    }

    /// <summary>
    /// Retorna uma pessoa específica por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<Pessoa>> GetById(int id)
    {
        var pessoa = await pessoaService.GetPessoaById(id);
        
        if (pessoa == null)
        {
            return NotFound(new { message = "Pessoa não encontrada." });
        }

        return Ok(pessoa);
    }

    /// <summary>
    /// Cria uma nova pessoa
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Pessoa>> Create([FromBody] CreatePessoaDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var pessoa = new Pessoa
        {
            Nome = dto.Nome,
            Email = dto.Email,
            CPF = dto.CPF,
            Telefone = dto.Telefone
        };

        var (success, message) = await pessoaService.CreatePessoa(pessoa);

        if (!success)
        {
            return BadRequest(new { message });
        }

        return CreatedAtAction(nameof(GetById), new { id = pessoa.Id }, pessoa);
    }

    /// <summary>
    /// Atualiza uma pessoa existente
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] UpdatePessoaDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var pessoa = new Pessoa
        {
            Nome = dto.Nome,
            Email = dto.Email,
            CPF = dto.CPF,
            Telefone = dto.Telefone
        };

        var (success, message) = await pessoaService.UpdatePessoa(id, pessoa);

        if (!success)
        {
            return NotFound(new { message });
        }

        return Ok(new { message });
    }

    /// <summary>
    /// Exclui uma pessoa
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var (success, message) = await pessoaService.DeletePessoa(id);

        if (!success)
        {
            return NotFound(new { message });
        }

        return Ok(new { message });
    }
}
