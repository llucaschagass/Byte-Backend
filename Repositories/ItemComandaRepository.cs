using Byte_Backend.Dados;
using Byte_Backend.Entidades;
using Byte_Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Byte_Backend.Repositories;

public class ItemComandaRepository : IItemComandaRepository
{
    private readonly ByteDbContext context;

    public ItemComandaRepository(ByteDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<ItemComanda>> GetAllItensComanda()
    {
        return await context.ItensComanda
            .Include(ic => ic.Comanda)
            .Include(ic => ic.Produto)
                .ThenInclude(p => p!.Categoria)
            .Include(ic => ic.OpcaoProduto)
            .Include(ic => ic.FilaCozinha)
            .OrderBy(ic => ic.Id)
            .ToListAsync();
    }

    public async Task<ItemComanda?> GetItemComandaById(int id)
    {
        return await context.ItensComanda
            .Include(ic => ic.Comanda)
                .ThenInclude(c => c!.Cliente)
                    .ThenInclude(cl => cl!.Pessoa)
            .Include(ic => ic.Comanda)
                .ThenInclude(c => c!.Cartao)
            .Include(ic => ic.Produto)
                .ThenInclude(p => p!.Categoria)
            .Include(ic => ic.OpcaoProduto)
            .Include(ic => ic.FilaCozinha)
            .FirstOrDefaultAsync(ic => ic.Id == id);
    }

    public async Task<IEnumerable<ItemComanda>> GetItensByComandaId(int comandaId)
    {
        return await context.ItensComanda
            .Include(ic => ic.Comanda)
            .Include(ic => ic.Produto)
                .ThenInclude(p => p!.Categoria)
            .Include(ic => ic.OpcaoProduto)
            .Include(ic => ic.FilaCozinha)
            .Where(ic => ic.ComandaId == comandaId)
            .OrderBy(ic => ic.Id)
            .ToListAsync();
    }

    public async Task CreateItemComanda(ItemComanda item)
    {
        item.InseridoEm = DateTime.UtcNow;
        await context.ItensComanda.AddAsync(item);
        await context.SaveChangesAsync();
    }

    public async Task UpdateItemComanda(ItemComanda item)
    {
        context.ItensComanda.Update(item);
        await context.SaveChangesAsync();
    }

    public async Task DeleteItemComanda(int id)
    {
        var item = await GetItemComandaById(id);
        if (item != null)
        {
            context.ItensComanda.Remove(item);
            await context.SaveChangesAsync();
        }
    }
}
