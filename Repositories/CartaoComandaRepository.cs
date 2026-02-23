using Byte_Backend.Dados;
using Byte_Backend.Entidades;
using Byte_Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Byte_Backend.Repositories;

public class CartaoComandaRepository : ICartaoComandaRepository
{
    private readonly ByteDbContext context;

    public CartaoComandaRepository(ByteDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<CartaoComanda>> GetAllCartoesComanda()
    {
        return await context.CartoesComanda
            .Include(cc => cc.Comandas)
            .OrderBy(cc => cc.NumeroCartao)
            .ToListAsync();
    }

    public async Task<CartaoComanda?> GetCartaoComandaById(int id)
    {
        return await context.CartoesComanda
            .Include(cc => cc.Comandas)
            .FirstOrDefaultAsync(cc => cc.Id == id);
    }

    public async Task<CartaoComanda?> GetCartaoComandaByNumeroCartao(int numeroCartao)
    {
        return await context.CartoesComanda
            .Include(cc => cc.Comandas)
            .FirstOrDefaultAsync(cc => cc.NumeroCartao == numeroCartao);
    }

    public async Task<CartaoComanda?> GetCartaoComandaByCodigoRfid(string codigoRfid)
    {
        return await context.CartoesComanda
            .Include(cc => cc.Comandas)
            .FirstOrDefaultAsync(cc => cc.CodigoRfid == codigoRfid);
    }

    public async Task CreateCartaoComanda(CartaoComanda cartao)
    {
        cartao.InseridoEm = DateTime.UtcNow;
        await context.CartoesComanda.AddAsync(cartao);
        await context.SaveChangesAsync();
    }

    public async Task UpdateCartaoComanda(CartaoComanda cartao)
    {
        context.CartoesComanda.Update(cartao);
        await context.SaveChangesAsync();
    }

    public async Task DeleteCartaoComanda(int id)
    {
        var cartao = await GetCartaoComandaById(id);
        if (cartao != null)
        {
            context.CartoesComanda.Remove(cartao);
            await context.SaveChangesAsync();
        }
    }
}
