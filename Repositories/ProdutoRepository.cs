using Byte_Backend.Dados;
using Byte_Backend.Entidades;
using Byte_Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Byte_Backend.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly ByteDbContext context;

    public ProdutoRepository(ByteDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<Produto>> GetAllProdutos()
    {
        return await context.Produtos
            .Include(p => p.Categoria)
            .OrderBy(p => p.Nome)
            .ToListAsync();
    }

    public async Task<IEnumerable<Produto>> GetProdutosByCategoria(int categoriaId)
    {
        return await context.Produtos
            .Where(p => p.CategoriaId == categoriaId)
            .Include(p => p.Categoria)
            .OrderBy(p => p.Nome)
            .ToListAsync();
    }

    public async Task<Produto?> GetProdutoById(int id)
    {
        return await context.Produtos
            .Include(p => p.Categoria)
            .Include(p => p.Opcoes)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Produto?> GetProdutoByNome(string nome)
    {
        return await context.Produtos
            .Include(p => p.Categoria)
            .FirstOrDefaultAsync(p => p.Nome.ToLower() == nome.ToLower());
    }

    public async Task CreateProduto(Produto produto)
    {
        produto.InseridoEm = DateTime.UtcNow;
        await context.Produtos.AddAsync(produto);
        await context.SaveChangesAsync();
    }

    public async Task UpdateProduto(Produto produto)
    {
        context.Produtos.Update(produto);
        await context.SaveChangesAsync();
    }

    public async Task DeleteProduto(int id)
    {
        var produto = await GetProdutoById(id);
        if (produto != null)
        {
            context.Produtos.Remove(produto);
            await context.SaveChangesAsync();
        }
    }
}
