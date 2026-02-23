using Byte_Backend.Entidades;

namespace Byte_Backend.Interfaces;

public interface IProdutoImagemRepository
{
    Task<IEnumerable<ProdutoImagem>> GetAllImagensByProduto(int produtoId);
    Task<ProdutoImagem?> GetImagemById(int id);
    Task CreateImagem(ProdutoImagem imagem);
    Task DeleteImagem(int id);
}
