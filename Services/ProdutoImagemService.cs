using Byte_Backend.Entidades;
using Byte_Backend.Interfaces;

namespace Byte_Backend.Services;

public class ProdutoImagemService
{
    private readonly IProdutoImagemRepository produtoImagemRepository;
    private readonly IProdutoRepository produtoRepository;

    public ProdutoImagemService(IProdutoImagemRepository produtoImagemRepository, IProdutoRepository produtoRepository)
    {
        this.produtoImagemRepository = produtoImagemRepository;
        this.produtoRepository = produtoRepository;
    }

    public async Task<IEnumerable<ProdutoImagem>> GetAllImagensByProduto(int produtoId)
    {
        return await produtoImagemRepository.GetAllImagensByProduto(produtoId);
    }

    public async Task<ProdutoImagem?> GetImagemById(int id)
    {
        return await produtoImagemRepository.GetImagemById(id);
    }

    public async Task<(bool Success, string Message)> CreateImagem(ProdutoImagem imagem)
    {
        var produto = await produtoRepository.GetProdutoById(imagem.ProdutoId);
        if (produto == null)
        {
            return (false, $"Produto com ID {imagem.ProdutoId} não encontrado.");
        }

        await produtoImagemRepository.CreateImagem(imagem);
        return (true, "Imagem cadastrada com sucesso.");
    }

    public async Task<(bool Success, string Message)> DeleteImagem(int id)
    {
        var imagem = await produtoImagemRepository.GetImagemById(id);
        if (imagem == null)
        {
            return (false, "Imagem não encontrada.");
        }

        await produtoImagemRepository.DeleteImagem(id);
        return (true, "Imagem excluída com sucesso.");
    }
}
