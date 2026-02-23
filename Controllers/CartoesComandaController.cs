using Byte_Backend.DTOs;
using Byte_Backend.Entidades;
using Byte_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Byte_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CartoesComandaController : ControllerBase
{
    private readonly CartaoComandaService cartaoComandaService;

    public CartoesComandaController(CartaoComandaService cartaoComandaService)
    {
        this.cartaoComandaService = cartaoComandaService;
    }

    /// <summary>
    /// Retorna todos os cartões comanda cadastrados
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CartaoComanda>>> GetAll()
    {
        var cartoes = await cartaoComandaService.GetAllCartoesComanda();
        return Ok(cartoes);
    }

    /// <summary>
    /// Retorna um cartão comanda específico por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<CartaoComanda>> GetById(int id)
    {
        var cartao = await cartaoComandaService.GetCartaoComandaById(id);
        
        if (cartao == null)
        {
            return NotFound(new { message = "Cartão comanda não encontrado." });
        }

        return Ok(cartao);
    }

    /// <summary>
    /// Cria um novo cartão comanda
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<CartaoComanda>> Create([FromBody] CreateCartaoComandaDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var cartao = new CartaoComanda
        {
            NumeroCartao = dto.NumeroCartao,
            CodigoRfid = dto.CodigoRfid
        };

        var (success, message) = await cartaoComandaService.CreateCartaoComanda(cartao);

        if (!success)
        {
            return BadRequest(new { message });
        }

        return CreatedAtAction(nameof(GetById), new { id = cartao.Id }, cartao);
    }

    /// <summary>
    /// Atualiza um cartão comanda existente
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateCartaoComandaDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var cartao = new CartaoComanda
        {
            NumeroCartao = dto.NumeroCartao,
            CodigoRfid = dto.CodigoRfid
        };

        var (success, message) = await cartaoComandaService.UpdateCartaoComanda(id, cartao);

        if (!success)
        {
            return NotFound(new { message });
        }

        return Ok(new { message });
    }

    /// <summary>
    /// Exclui um cartão comanda
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var (success, message) = await cartaoComandaService.DeleteCartaoComanda(id);

        if (!success)
        {
            return NotFound(new { message });
        }

        return Ok(new { message });
    }
}
