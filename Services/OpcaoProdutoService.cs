using Byte_Backend.Entidades;
using Byte_Backend.Interfaces;

namespace Byte_Backend.Services;

public class OpcaoProdutoService
{
    private readonly IOpcaoProdutoRepository opcaoRepository;
    private readonly IProdutoRepository produtoRepository;

    public OpcaoProdutoService(IOpcaoProdutoRepository opcaoRepository, IProdutoRepository produtoRepository)
    {
        this.opcaoRepository = opcaoRepository;
        this.produtoRepository = produtoRepository;
    }

    public async Task<IEnumerable<OpcaoProduto>> GetAllOpcoes()
    {
        return await opcaoRepository.GetAllOpcoes();
    }

    public async Task<IEnumerable<OpcaoProduto>> GetOpcoesByProduto(int produtoId)
    {
        return await opcaoRepository.GetOpcoesByProduto(produtoId);
    }

    public async Task<OpcaoProduto?> GetOpcaoById(int id)
    {
        return await opcaoRepository.GetOpcaoById(id);
    }

    public async Task<(bool Success, string Message)> CreateOpcao(OpcaoProduto opcao)
    {
        // Verificar se o produto existe
        var produto = await produtoRepository.GetProdutoById(opcao.ProdutoId);
        if (produto == null)
        {
            return (false, "Produto não encontrado.");
        }

        await opcaoRepository.CreateOpcao(opcao);
        return (true, "Opção criada com sucesso.");
    }

    public async Task<(bool Success, string Message)> UpdateOpcao(int id, OpcaoProduto opcao)
    {
        var opcaoExistente = await opcaoRepository.GetOpcaoById(id);
        if (opcaoExistente == null)
        {
            return (false, "Opção não encontrada.");
        }

        // Verificar se o produto existe
        if (opcao.ProdutoId != opcaoExistente.ProdutoId)
        {
            var produto = await produtoRepository.GetProdutoById(opcao.ProdutoId);
            if (produto == null)
            {
                return (false, "Produto não encontrado.");
            }
        }

        opcaoExistente.ProdutoId = opcao.ProdutoId;
        opcaoExistente.Nome = opcao.Nome;
        opcaoExistente.PrecoAdicional = opcao.PrecoAdicional;

        await opcaoRepository.UpdateOpcao(opcaoExistente);
        return (true, "Opção atualizada com sucesso.");
    }

    public async Task<(bool Success, string Message)> DeleteOpcao(int id)
    {
        var opcao = await opcaoRepository.GetOpcaoById(id);
        if (opcao == null)
        {
            return (false, "Opção não encontrada.");
        }

        await opcaoRepository.DeleteOpcao(id);
        return (true, "Opção excluída com sucesso.");
    }
}
