using Byte_Backend.Dados;
using Byte_Backend.Entidades;
using Byte_Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Byte_Backend.Repositories;

public class FilaCozinhaRepository : IFilaCozinhaRepository
{
    private readonly ByteDbContext context;

    public FilaCozinhaRepository(ByteDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<FilaCozinha>> GetAllFilasCozinha()
    {
        return await context.FilaCozinha
            .Include(f => f.ItemComanda)
                .ThenInclude(i => i!.Produto)
            .Include(f => f.ItemComanda)
                .ThenInclude(i => i!.OpcaoProduto)
            .Include(f => f.ItemComanda)
                .ThenInclude(i => i!.Comanda)
            .OrderByDescending(f => f.UltimaAtualizacao)
            .ToListAsync();
    }

    public async Task<FilaCozinha?> GetFilaCozinhaById(int id)
    {
        return await context.FilaCozinha
            .Include(f => f.ItemComanda)
                .ThenInclude(i => i!.Produto)
            .Include(f => f.ItemComanda)
                .ThenInclude(i => i!.OpcaoProduto)
            .Include(f => f.ItemComanda)
                .ThenInclude(i => i!.Comanda)
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<IEnumerable<FilaCozinha>> GetFilasCozinhaByStatus(string status)
    {
        return await context.FilaCozinha
            .Include(f => f.ItemComanda)
                .ThenInclude(i => i!.Produto)
            .Include(f => f.ItemComanda)
                .ThenInclude(i => i!.OpcaoProduto)
            .Include(f => f.ItemComanda)
                .ThenInclude(i => i!.Comanda)
            .Where(f => f.StatusPreparo == status)
            .OrderByDescending(f => f.UltimaAtualizacao)
            .ToListAsync();
    }

    public async Task<IEnumerable<FilaCozinha>> GetFilasCozinhaByItemComandaId(int itemComandaId)
    {
        return await context.FilaCozinha
            .Include(f => f.ItemComanda)
                .ThenInclude(i => i!.Produto)
            .Include(f => f.ItemComanda)
                .ThenInclude(i => i!.OpcaoProduto)
            .Include(f => f.ItemComanda)
                .ThenInclude(i => i!.Comanda)
            .Where(f => f.ItemComandaId == itemComandaId)
            .OrderByDescending(f => f.UltimaAtualizacao)
            .ToListAsync();
    }

    public async Task CreateFilaCozinha(FilaCozinha filaCozinha)
    {
        filaCozinha.InseridoEm = DateTime.UtcNow;
        filaCozinha.UltimaAtualizacao = DateTime.UtcNow;
        await context.FilaCozinha.AddAsync(filaCozinha);
        await context.SaveChangesAsync();
    }

    public async Task UpdateFilaCozinha(FilaCozinha filaCozinha)
    {
        filaCozinha.UltimaAtualizacao = DateTime.UtcNow;
        context.FilaCozinha.Update(filaCozinha);
        await context.SaveChangesAsync();
    }

    public async Task DeleteFilaCozinha(int id)
    {
        var filaCozinha = await GetFilaCozinhaById(id);
        if (filaCozinha != null)
        {
            context.FilaCozinha.Remove(filaCozinha);
            await context.SaveChangesAsync();
        }
    }
}
