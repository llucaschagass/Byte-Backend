using Byte_Backend.Entidades;

namespace Byte_Backend.Interfaces;

public interface IOpcaoProdutoRepository
{
    Task<IEnumerable<OpcaoProduto>> GetAllOpcoes();
    Task<IEnumerable<OpcaoProduto>> GetOpcoesByProduto(int produtoId);
    Task<OpcaoProduto?> GetOpcaoById(int id);
    Task CreateOpcao(OpcaoProduto opcao);
    Task UpdateOpcao(OpcaoProduto opcao);
    Task DeleteOpcao(int id);
}
