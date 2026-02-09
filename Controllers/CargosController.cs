using Byte_Backend.DTOs;
using Byte_Backend.Entidades;
using Byte_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Byte_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CargosController : ControllerBase
{
    private readonly CargoService cargoService;

    public CargosController(CargoService cargoService)
    {
        this.cargoService = cargoService;
    }

    /// <summary>
    /// Retorna todos os cargos cadastrados
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cargo>>> GetAll()
    {
        var cargos = await cargoService.GetAllCargos();
        return Ok(cargos);
    }

    /// <summary>
    /// Retorna um cargo específico por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<Cargo>> GetById(int id)
    {
        var cargo = await cargoService.GetCargoById(id);
        
        if (cargo == null)
        {
            return NotFound(new { message = "Cargo não encontrado." });
        }

        return Ok(cargo);
    }

    /// <summary>
    /// Cria um novo cargo
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Cargo>> Create([FromBody] CreateCargoDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var cargo = new Cargo
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao
        };

        var (success, message) = await cargoService.CreateCargo(cargo);

        if (!success)
        {
            return BadRequest(new { message });
        }

        return CreatedAtAction(nameof(GetById), new { id = cargo.Id }, cargo);
    }

    /// <summary>
    /// Atualiza um cargo existente
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateCargoDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var cargo = new Cargo
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao
        };

        var (success, message) = await cargoService.UpdateCargo(id, cargo);

        if (!success)
        {
            return NotFound(new { message });
        }

        return Ok(new { message });
    }

    /// <summary>
    /// Exclui um cargo
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var (success, message) = await cargoService.DeleteCargo(id);

        if (!success)
        {
            return NotFound(new { message });
        }

        return Ok(new { message });
    }
}
