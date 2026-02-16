using Byte_Backend.Dados;
using Byte_Backend.Entidades;
using Byte_Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Byte_Backend.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly ByteDbContext context;

    public CategoriaRepository(ByteDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<Categoria>> GetAllCategorias()
    {
        return await context.Categorias
            .OrderBy(c => c.Nome)
            .ToListAsync();
    }

    public async Task<Categoria?> GetCategoriaById(int id)
    {
        return await context.Categorias
            .FirstOrDefaultAsync(c => c.CategoriaId == id);
    }

    public async Task<Categoria?> GetCategoriaByNome(string nome)
    {
        return await context.Categorias
            .FirstOrDefaultAsync(c => c.Nome.ToLower() == nome.ToLower());
    }

    public async Task CreateCategoria(Categoria categoria)
    {
        categoria.InseridoEm = DateTime.Now;
        await context.Categorias.AddAsync(categoria);
        await context.SaveChangesAsync();
    }

    public async Task UpdateCategoria(Categoria categoria)
    {
        context.Categorias.Update(categoria);
        await context.SaveChangesAsync();
    }

    public async Task DeleteCategoria(int id)
    {
        var categoria = await GetCategoriaById(id);
        if (categoria != null)
        {
            context.Categorias.Remove(categoria);
            await context.SaveChangesAsync();
        }
    }
}
