using Byte_Backend.Dados;
using Byte_Backend.Entidades;
using Byte_Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Byte_Backend.Repositories;

public class ReceitaRepository : IReceitaRepository
{
    private readonly ByteDbContext context;

    public ReceitaRepository(ByteDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<Receita>> GetAllReceitas()
    {
        return await context.Receitas
            .Include(r => r.Produto)
            .Include(r => r.Ingredientes)
            .OrderBy(r => r.Descricao)
            .ToListAsync();
    }

    public async Task<Receita?> GetReceitaById(int id)
    {
        return await context.Receitas
            .Include(r => r.Produto)
            .Include(r => r.Ingredientes)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Receita?> GetReceitaByProdutoId(int produtoId)
    {
        return await context.Receitas
            .Include(r => r.Produto)
            .Include(r => r.Ingredientes)
            .FirstOrDefaultAsync(r => r.ProdutoId == produtoId);
    }

    public async Task CreateReceita(Receita receita)
    {
        receita.InseridoEm = DateTime.UtcNow;
        await context.Receitas.AddAsync(receita);
        await context.SaveChangesAsync();
    }

    public async Task UpdateReceita(Receita receita)
    {
        context.Receitas.Update(receita);
        await context.SaveChangesAsync();
    }

    public async Task DeleteReceita(int id)
    {
        var receita = await GetReceitaById(id);
        if (receita != null)
        {
            context.Receitas.Remove(receita);
            await context.SaveChangesAsync();
        }
    }
}
