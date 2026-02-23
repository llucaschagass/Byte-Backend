using Byte_Backend.Entidades;

namespace Byte_Backend.Interfaces;

public interface IProdutoRepository
{
    Task<IEnumerable<Produto>> GetAllProdutos();
    Task<IEnumerable<Produto>> GetProdutosByCategoria(int categoriaId);
    Task<Produto?> GetProdutoById(int id);
    Task<Produto?> GetProdutoByNome(string nome);
    Task CreateProduto(Produto produto);
    Task UpdateProduto(Produto produto);
    Task DeleteProduto(int id);
}
