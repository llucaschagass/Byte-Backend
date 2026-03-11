using Byte_Backend.Dados;
using Byte_Backend.Entidades;
using Byte_Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Byte_Backend.Repositories;

public class IngredienteReceitaRepository : IIngredienteReceitaRepository
{
    private readonly ByteDbContext context;

    public IngredienteReceitaRepository(ByteDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<IngredienteReceita>> GetByReceitaId(int receitaId)
    {
        return await context.IngredientesReceitas
            .Where(i => i.ReceitaId == receitaId)
            .Include(i => i.Receita)
            .OrderBy(i => i.Nome)
            .ToListAsync();
    }

    public async Task<IngredienteReceita?> GetById(int id)
    {
        return await context.IngredientesReceitas
            .Include(i => i.Receita)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task Create(IngredienteReceita ingrediente)
    {
        ingrediente.InseridoEm = DateTime.UtcNow;
        await context.IngredientesReceitas.AddAsync(ingrediente);
        await context.SaveChangesAsync();
    }

    public async Task Update(IngredienteReceita ingrediente)
    {
        context.IngredientesReceitas.Update(ingrediente);
        await context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var ingrediente = await GetById(id);
        if (ingrediente != null)
        {
            context.IngredientesReceitas.Remove(ingrediente);
            await context.SaveChangesAsync();
        }
    }
}
