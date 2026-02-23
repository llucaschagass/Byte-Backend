using Byte_Backend.Dados;
using Byte_Backend.Entidades;
using Byte_Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Byte_Backend.Repositories;

public class ProdutoImagemRepository : IProdutoImagemRepository
{
    private readonly ByteDbContext context;

    public ProdutoImagemRepository(ByteDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<ProdutoImagem>> GetAllImagensByProduto(int produtoId)
    {
        return await context.ProdutosImagens
            .Where(pi => pi.ProdutoId == produtoId)
            .OrderBy(pi => pi.InseridoEm)
            .ToListAsync();
    }

    public async Task<ProdutoImagem?> GetImagemById(int id)
    {
        return await context.ProdutosImagens
            .FirstOrDefaultAsync(pi => pi.ProdutoImagemId == id);
    }

    public async Task CreateImagem(ProdutoImagem imagem)
    {
        imagem.InseridoEm = DateTime.UtcNow;
        await context.ProdutosImagens.AddAsync(imagem);
        await context.SaveChangesAsync();
    }

    public async Task DeleteImagem(int id)
    {
        var imagem = await GetImagemById(id);
        if (imagem != null)
        {
            context.ProdutosImagens.Remove(imagem);
            await context.SaveChangesAsync();
        }
    }
}
