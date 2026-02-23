using Byte_Backend.Entidades;
using Byte_Backend.Interfaces;

namespace Byte_Backend.Services;

public class ItemComandaService
{
    private readonly IItemComandaRepository itemComandaRepository;
    private readonly IComandaRepository comandaRepository;
    private readonly IProdutoRepository produtoRepository;
    private readonly IOpcaoProdutoRepository opcaoProdutoRepository;

    public ItemComandaService(
        IItemComandaRepository itemComandaRepository,
        IComandaRepository comandaRepository,
        IProdutoRepository produtoRepository,
        IOpcaoProdutoRepository opcaoProdutoRepository)
    {
        this.itemComandaRepository = itemComandaRepository;
        this.comandaRepository = comandaRepository;
        this.produtoRepository = produtoRepository;
        this.opcaoProdutoRepository = opcaoProdutoRepository;
    }

    public async Task<IEnumerable<ItemComanda>> GetAllItensComanda()
    {
        return await itemComandaRepository.GetAllItensComanda();
    }

    public async Task<ItemComanda?> GetItemComandaById(int id)
    {
        return await itemComandaRepository.GetItemComandaById(id);
    }

    public async Task<IEnumerable<ItemComanda>> GetItensByComandaId(int comandaId)
    {
        return await itemComandaRepository.GetItensByComandaId(comandaId);
    }

    public async Task<(bool Success, string Message)> CreateItemComanda(ItemComanda item)
    {
        // Verificar se a comanda existe
        var comanda = await comandaRepository.GetComandaById(item.ComandaId);
        if (comanda == null)
        {
            return (false, "Comanda não encontrada.");
        }

        // Verificar se a comanda está aberta
        if (comanda.Status != "Aberta")
        {
            return (false, "Não é possível adicionar itens a uma comanda fechada.");
        }

        // Verificar se o produto existe
        var produto = await produtoRepository.GetProdutoById(item.ProdutoId);
        if (produto == null)
        {
            return (false, "Produto não encontrado.");
        }

        // Verificar se o produto está ativo
        if (!produto.Ativo)
        {
            return (false, $"O produto '{produto.Nome}' não está disponível.");
        }

        // Se foi informada uma opção de produto, verificar se existe e pertence ao produto
        if (item.OpcaoProdutoId.HasValue)
        {
            var opcaoProduto = await opcaoProdutoRepository.GetOpcaoById(item.OpcaoProdutoId.Value);
            if (opcaoProduto == null)
            {
                return (false, "Opção de produto não encontrada.");
            }

            if (opcaoProduto.ProdutoId != item.ProdutoId)
            {
                return (false, "A opção selecionada não pertence ao produto escolhido.");
            }
        }

        // Validar quantidade
        if (item.Quantidade <= 0)
        {
            return (false, "A quantidade deve ser maior que zero.");
        }

        // Validar preço unitário
        if (item.PrecoUnitario < 0)
        {
            return (false, "O preço unitário não pode ser negativo.");
        }

        await itemComandaRepository.CreateItemComanda(item);
        return (true, "Item adicionado à comanda com sucesso.");
    }

    public async Task<(bool Success, string Message)> UpdateItemComanda(int id, ItemComanda item)
    {
        var itemExistente = await itemComandaRepository.GetItemComandaById(id);
        if (itemExistente == null)
        {
            return (false, "Item da comanda não encontrado.");
        }

        // Verificar se a comanda existe
        var comanda = await comandaRepository.GetComandaById(item.ComandaId);
        if (comanda == null)
        {
            return (false, "Comanda não encontrada.");
        }

        // Verificar se a comanda está aberta
        if (comanda.Status != "Aberta")
        {
            return (false, "Não é possível modificar itens de uma comanda fechada.");
        }

        // Verificar se o produto existe
        var produto = await produtoRepository.GetProdutoById(item.ProdutoId);
        if (produto == null)
        {
            return (false, "Produto não encontrado.");
        }

        // Se foi informada uma opção de produto, verificar se existe e pertence ao produto
        if (item.OpcaoProdutoId.HasValue)
        {
            var opcaoProduto = await opcaoProdutoRepository.GetOpcaoById(item.OpcaoProdutoId.Value);
            if (opcaoProduto == null)
            {
                return (false, "Opção de produto não encontrada.");
            }

            if (opcaoProduto.ProdutoId != item.ProdutoId)
            {
                return (false, "A opção selecionada não pertence ao produto escolhido.");
            }
        }

        // Validar quantidade
        if (item.Quantidade <= 0)
        {
            return (false, "A quantidade deve ser maior que zero.");
        }

        // Validar preço unitário
        if (item.PrecoUnitario < 0)
        {
            return (false, "O preço unitário não pode ser negativo.");
        }

        itemExistente.ComandaId = item.ComandaId;
        itemExistente.ProdutoId = item.ProdutoId;
        itemExistente.OpcaoProdutoId = item.OpcaoProdutoId;
        itemExistente.Quantidade = item.Quantidade;
        itemExistente.PrecoUnitario = item.PrecoUnitario;
        itemExistente.Observacao = item.Observacao;

        await itemComandaRepository.UpdateItemComanda(itemExistente);
        return (true, "Item da comanda atualizado com sucesso.");
    }

    public async Task<(bool Success, string Message)> DeleteItemComanda(int id)
    {
        var item = await itemComandaRepository.GetItemComandaById(id);
        if (item == null)
        {
            return (false, "Item da comanda não encontrado.");
        }

        // Verificar se a comanda está aberta
        var comanda = await comandaRepository.GetComandaById(item.ComandaId);
        if (comanda != null && comanda.Status != "Aberta")
        {
            return (false, "Não é possível remover itens de uma comanda fechada.");
        }

        await itemComandaRepository.DeleteItemComanda(id);
        return (true, "Item removido da comanda com sucesso.");
    }
}
