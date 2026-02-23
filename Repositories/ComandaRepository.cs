using Byte_Backend.Dados;
using Byte_Backend.Entidades;
using Byte_Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Byte_Backend.Repositories;

public class ComandaRepository : IComandaRepository
{
    private readonly ByteDbContext context;

    public ComandaRepository(ByteDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<Comanda>> GetAllComandas()
    {
        return await context.Comandas
            .Include(c => c.Cartao)
            .Include(c => c.Cliente)
                .ThenInclude(cl => cl!.Pessoa)
            .Include(c => c.Itens)
            .OrderByDescending(c => c.AbertaEm)
            .ToListAsync();
    }

    public async Task<Comanda?> GetComandaById(int id)
    {
        return await context.Comandas
            .Include(c => c.Cartao)
            .Include(c => c.Cliente)
                .ThenInclude(cl => cl!.Pessoa)
            .Include(c => c.Itens)
                .ThenInclude(i => i.Produto)
            .Include(c => c.Itens)
                .ThenInclude(i => i.OpcaoProduto)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Comanda>> GetComandasByClienteId(int clienteId)
    {
        return await context.Comandas
            .Include(c => c.Cartao)
            .Include(c => c.Cliente)
                .ThenInclude(cl => cl!.Pessoa)
            .Include(c => c.Itens)
            .Where(c => c.ClienteId == clienteId)
            .OrderByDescending(c => c.AbertaEm)
            .ToListAsync();
    }

    public async Task<IEnumerable<Comanda>> GetComandasByCartaoId(int cartaoId)
    {
        return await context.Comandas
            .Include(c => c.Cartao)
            .Include(c => c.Cliente)
                .ThenInclude(cl => cl!.Pessoa)
            .Include(c => c.Itens)
            .Where(c => c.CartaoId == cartaoId)
            .OrderByDescending(c => c.AbertaEm)
            .ToListAsync();
    }

    public async Task<Comanda?> GetComandaAbertaByCartaoId(int cartaoId)
    {
        return await context.Comandas
            .Include(c => c.Cartao)
            .Include(c => c.Cliente)
                .ThenInclude(cl => cl!.Pessoa)
            .Include(c => c.Itens)
            .Where(c => c.CartaoId == cartaoId && c.Status == "Aberta")
            .FirstOrDefaultAsync();
    }

    public async Task CreateComanda(Comanda comanda)
    {
        comanda.InseridoEm = DateTime.UtcNow;
        await context.Comandas.AddAsync(comanda);
        await context.SaveChangesAsync();
    }

    public async Task UpdateComanda(Comanda comanda)
    {
        context.Comandas.Update(comanda);
        await context.SaveChangesAsync();
    }

    public async Task DeleteComanda(int id)
    {
        var comanda = await GetComandaById(id);
        if (comanda != null)
        {
            context.Comandas.Remove(comanda);
            await context.SaveChangesAsync();
        }
    }
}
