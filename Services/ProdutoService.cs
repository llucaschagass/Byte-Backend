using Byte_Backend.Entidades;
using Byte_Backend.Interfaces;

namespace Byte_Backend.Services;

public class ProdutoService
{
    private readonly IProdutoRepository produtoRepository;
    private readonly ICategoriaRepository categoriaRepository;

    public ProdutoService(IProdutoRepository produtoRepository, ICategoriaRepository categoriaRepository)
    {
        this.produtoRepository = produtoRepository;
        this.categoriaRepository = categoriaRepository;
    }

    public async Task<IEnumerable<Produto>> GetAllProdutos()
    {
        return await produtoRepository.GetAllProdutos();
    }

    public async Task<IEnumerable<Produto>> GetProdutosByCategoria(int categoriaId)
    {
        return await produtoRepository.GetProdutosByCategoria(categoriaId);
    }

    public async Task<Produto?> GetProdutoById(int id)
    {
        return await produtoRepository.GetProdutoById(id);
    }

    public async Task<(bool Success, string Message)> CreateProduto(Produto produto)
    {
        // Verificar se a categoria existe
        var categoria = await categoriaRepository.GetCategoriaById(produto.CategoriaId);
        if (categoria == null)
        {
            return (false, "Categoria não encontrada.");
        }

        // Verificar se já existe um produto com o mesmo nome
        var produtoExistente = await produtoRepository.GetProdutoByNome(produto.Nome);
        if (produtoExistente != null)
        {
            return (false, $"Já existe um produto com o nome '{produto.Nome}'.");
        }

        await produtoRepository.CreateProduto(produto);
        return (true, "Produto criado com sucesso.");
    }

    public async Task<(bool Success, string Message)> UpdateProduto(int id, Produto produto)
    {
        var produtoExistente = await produtoRepository.GetProdutoById(id);
        if (produtoExistente == null)
        {
            return (false, "Produto não encontrado.");
        }
        
        // Verificar se a categoria existe
        if (produto.CategoriaId != produtoExistente.CategoriaId)
        {
            var categoria = await categoriaRepository.GetCategoriaById(produto.CategoriaId);
            if (categoria == null)
            {
                return (false, "Categoria não encontrada.");
            }
        }

        // Verificar duplicidade de nome
        var produtoComMesmoNome = await produtoRepository.GetProdutoByNome(produto.Nome);
        if (produtoComMesmoNome != null && produtoComMesmoNome.Id != id)
        {
             return (false, $"Já existe outro produto com o nome '{produto.Nome}'.");
        }

        produtoExistente.CategoriaId = produto.CategoriaId;
        produtoExistente.Nome = produto.Nome;
        produtoExistente.Preco = produto.Preco;
        produtoExistente.Descricao = produto.Descricao;
        produtoExistente.Ativo = produto.Ativo;

        await produtoRepository.UpdateProduto(produtoExistente);
        return (true, "Produto atualizado com sucesso.");
    }

    public async Task<(bool Success, string Message)> DeleteProduto(int id)
    {
        var produto = await produtoRepository.GetProdutoById(id);
        if (produto == null)
        {
            return (false, "Produto não encontrado.");
        }

        await produtoRepository.DeleteProduto(id);
        return (true, "Produto excluído com sucesso.");
    }
}
