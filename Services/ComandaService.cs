using Byte_Backend.Entidades;
using Byte_Backend.Interfaces;

namespace Byte_Backend.Services;

public class ComandaService
{
    private readonly IComandaRepository comandaRepository;
    private readonly ICartaoComandaRepository cartaoComandaRepository;
    private readonly IClienteRepository clienteRepository;

    public ComandaService(
        IComandaRepository comandaRepository,
        ICartaoComandaRepository cartaoComandaRepository,
        IClienteRepository clienteRepository)
    {
        this.comandaRepository = comandaRepository;
        this.cartaoComandaRepository = cartaoComandaRepository;
        this.clienteRepository = clienteRepository;
    }

    public async Task<IEnumerable<Comanda>> GetAllComandas()
    {
        return await comandaRepository.GetAllComandas();
    }

    public async Task<Comanda?> GetComandaById(int id)
    {
        return await comandaRepository.GetComandaById(id);
    }

    public async Task<IEnumerable<Comanda>> GetComandasByClienteId(int clienteId)
    {
        return await comandaRepository.GetComandasByClienteId(clienteId);
    }

    public async Task<IEnumerable<Comanda>> GetComandasByCartaoId(int cartaoId)
    {
        return await comandaRepository.GetComandasByCartaoId(cartaoId);
    }

    public async Task<(bool Success, string Message)> CreateComanda(Comanda comanda)
    {
        // Verificar se o cartão existe
        var cartao = await cartaoComandaRepository.GetCartaoComandaById(comanda.CartaoId);
        if (cartao == null)
        {
            return (false, "Cartão comanda não encontrado.");
        }

        // Verificar se o cliente existe
        var cliente = await clienteRepository.GetClienteById(comanda.ClienteId);
        if (cliente == null)
        {
            return (false, "Cliente não encontrado.");
        }

        // Verificar se já existe uma comanda aberta para este cartão
        var comandaAberta = await comandaRepository.GetComandaAbertaByCartaoId(comanda.CartaoId);
        if (comandaAberta != null)
        {
            return (false, $"Já existe uma comanda aberta para o cartão número '{cartao.NumeroCartao}'.");
        }

        // Validar status
        if (comanda.Status != "Aberta" && comanda.Status != "Fechada")
        {
            return (false, "Status inválido. Use 'Aberta' ou 'Fechada'.");
        }

        // Se a comanda está sendo criada como fechada, definir FechadaEm
        if (comanda.Status == "Fechada" && comanda.FechadaEm == null)
        {
            comanda.FechadaEm = DateTime.UtcNow;
        }

        await comandaRepository.CreateComanda(comanda);
        return (true, "Comanda criada com sucesso.");
    }

    public async Task<(bool Success, string Message)> UpdateComanda(int id, Comanda comanda)
    {
        var comandaExistente = await comandaRepository.GetComandaById(id);
        if (comandaExistente == null)
        {
            return (false, "Comanda não encontrada.");
        }

        // Verificar se o cartão existe
        var cartao = await cartaoComandaRepository.GetCartaoComandaById(comanda.CartaoId);
        if (cartao == null)
        {
            return (false, "Cartão comanda não encontrado.");
        }

        // Verificar se o cliente existe
        var cliente = await clienteRepository.GetClienteById(comanda.ClienteId);
        if (cliente == null)
        {
            return (false, "Cliente não encontrado.");
        }

        // Se está mudando o cartão, verificar se não há comanda aberta no novo cartão
        if (comanda.CartaoId != comandaExistente.CartaoId)
        {
            var comandaAbertaNovoCartao = await comandaRepository.GetComandaAbertaByCartaoId(comanda.CartaoId);
            if (comandaAbertaNovoCartao != null && comandaAbertaNovoCartao.Id != id)
            {
                return (false, $"Já existe uma comanda aberta para o cartão número '{cartao.NumeroCartao}'.");
            }
        }

        // Validar status
        if (comanda.Status != "Aberta" && comanda.Status != "Fechada")
        {
            return (false, "Status inválido. Use 'Aberta' ou 'Fechada'.");
        }

        // Se está fechando a comanda, definir FechadaEm
        if (comanda.Status == "Fechada" && comandaExistente.Status == "Aberta")
        {
            comanda.FechadaEm = DateTime.UtcNow;
        }

        // Se está reabrindo a comanda, limpar FechadaEm
        if (comanda.Status == "Aberta" && comandaExistente.Status == "Fechada")
        {
            comanda.FechadaEm = null;
        }

        comandaExistente.CartaoId = comanda.CartaoId;
        comandaExistente.ClienteId = comanda.ClienteId;
        comandaExistente.Status = comanda.Status;
        comandaExistente.AbertaEm = comanda.AbertaEm;
        comandaExistente.FechadaEm = comanda.FechadaEm;

        await comandaRepository.UpdateComanda(comandaExistente);
        return (true, "Comanda atualizada com sucesso.");
    }

    public async Task<(bool Success, string Message)> DeleteComanda(int id)
    {
        var comanda = await comandaRepository.GetComandaById(id);
        if (comanda == null)
        {
            return (false, "Comanda não encontrada.");
        }

        await comandaRepository.DeleteComanda(id);
        return (true, "Comanda excluída com sucesso.");
    }

    public async Task<(bool Success, string Message)> FecharComanda(int id)
    {
        var comanda = await comandaRepository.GetComandaById(id);
        if (comanda == null)
        {
            return (false, "Comanda não encontrada.");
        }

        if (comanda.Status == "Fechada")
        {
            return (false, "Comanda já está fechada.");
        }

        comanda.Status = "Fechada";
        comanda.FechadaEm = DateTime.UtcNow;

        await comandaRepository.UpdateComanda(comanda);
        return (true, "Comanda fechada com sucesso.");
    }
}
