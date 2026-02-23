using Byte_Backend.Entidades;
using Byte_Backend.Interfaces;

namespace Byte_Backend.Services;

public class CartaoComandaService
{
    private readonly ICartaoComandaRepository cartaoComandaRepository;

    public CartaoComandaService(ICartaoComandaRepository cartaoComandaRepository)
    {
        this.cartaoComandaRepository = cartaoComandaRepository;
    }

    public async Task<IEnumerable<CartaoComanda>> GetAllCartoesComanda()
    {
        return await cartaoComandaRepository.GetAllCartoesComanda();
    }

    public async Task<CartaoComanda?> GetCartaoComandaById(int id)
    {
        return await cartaoComandaRepository.GetCartaoComandaById(id);
    }

    public async Task<(bool Success, string Message)> CreateCartaoComanda(CartaoComanda cartao)
    {
        // Verificar se já existe um cartão com o mesmo número
        var cartaoExistenteNumero = await cartaoComandaRepository.GetCartaoComandaByNumeroCartao(cartao.NumeroCartao);
        if (cartaoExistenteNumero != null)
        {
            return (false, $"Já existe um cartão cadastrado com o número '{cartao.NumeroCartao}'.");
        }

        // Verificar se já existe um cartão com o mesmo código RFID
        var cartaoExistenteRfid = await cartaoComandaRepository.GetCartaoComandaByCodigoRfid(cartao.CodigoRfid);
        if (cartaoExistenteRfid != null)
        {
            return (false, $"Já existe um cartão cadastrado com o código RFID '{cartao.CodigoRfid}'.");
        }

        await cartaoComandaRepository.CreateCartaoComanda(cartao);
        return (true, "Cartão comanda criado com sucesso.");
    }

    public async Task<(bool Success, string Message)> UpdateCartaoComanda(int id, CartaoComanda cartao)
    {
        var cartaoExistente = await cartaoComandaRepository.GetCartaoComandaById(id);
        if (cartaoExistente == null)
        {
            return (false, "Cartão comanda não encontrado.");
        }

        // Verificar se já existe outro cartão com o mesmo número
        var cartaoComMesmoNumero = await cartaoComandaRepository.GetCartaoComandaByNumeroCartao(cartao.NumeroCartao);
        if (cartaoComMesmoNumero != null && cartaoComMesmoNumero.Id != id)
        {
            return (false, $"Já existe outro cartão cadastrado com o número '{cartao.NumeroCartao}'.");
        }

        // Verificar se já existe outro cartão com o mesmo código RFID
        var cartaoComMesmoRfid = await cartaoComandaRepository.GetCartaoComandaByCodigoRfid(cartao.CodigoRfid);
        if (cartaoComMesmoRfid != null && cartaoComMesmoRfid.Id != id)
        {
            return (false, $"Já existe outro cartão cadastrado com o código RFID '{cartao.CodigoRfid}'.");
        }

        cartaoExistente.NumeroCartao = cartao.NumeroCartao;
        cartaoExistente.CodigoRfid = cartao.CodigoRfid;

        await cartaoComandaRepository.UpdateCartaoComanda(cartaoExistente);
        return (true, "Cartão comanda atualizado com sucesso.");
    }

    public async Task<(bool Success, string Message)> DeleteCartaoComanda(int id)
    {
        var cartao = await cartaoComandaRepository.GetCartaoComandaById(id);
        if (cartao == null)
        {
            return (false, "Cartão comanda não encontrado.");
        }

        await cartaoComandaRepository.DeleteCartaoComanda(id);
        return (true, "Cartão comanda excluído com sucesso.");
    }
}
