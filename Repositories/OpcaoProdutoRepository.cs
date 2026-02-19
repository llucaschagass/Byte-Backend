using Byte_Backend.Dados;
using Byte_Backend.Entidades;
using Byte_Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Byte_Backend.Repositories;

public class OpcaoProdutoRepository : IOpcaoProdutoRepository
{
    private readonly ByteDbContext context;

    public OpcaoProdutoRepository(ByteDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<OpcaoProduto>> GetAllOpcoes()
    {
        return await context.OpcoesProdutos
            .Include(op => op.Produto)
            .OrderBy(op => op.Nome)
            .ToListAsync();
    }

    public async Task<IEnumerable<OpcaoProduto>> GetOpcoesByProduto(int produtoId)
    {
        return await context.OpcoesProdutos
            .Where(op => op.ProdutoId == produtoId)
            .Include(op => op.Produto)
            .OrderBy(op => op.Nome)
            .ToListAsync();
    }

    public async Task<OpcaoProduto?> GetOpcaoById(int id)
    {
        return await context.OpcoesProdutos
            .Include(op => op.Produto)
            .FirstOrDefaultAsync(op => op.Id == id);
    }

    public async Task CreateOpcao(OpcaoProduto opcao)
    {
        opcao.InseridoEm = DateTime.UtcNow;
        await context.OpcoesProdutos.AddAsync(opcao);
        await context.SaveChangesAsync();
    }

    public async Task UpdateOpcao(OpcaoProduto opcao)
    {
        context.OpcoesProdutos.Update(opcao);
        await context.SaveChangesAsync();
    }

    public async Task DeleteOpcao(int id)
    {
        var opcao = await GetOpcaoById(id);
        if (opcao != null)
        {
            context.OpcoesProdutos.Remove(opcao);
            await context.SaveChangesAsync();
        }
    }
}
