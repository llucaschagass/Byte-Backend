using Byte_Backend.Dados;
using Byte_Backend.Entidades;
using Byte_Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Byte_Backend.Repositories;

public class CargoRepository : ICargoRepository
{
    private readonly ByteDbContext context;

    public CargoRepository(ByteDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<Cargo>> GetAllCargos()
    {
        return await context.Cargos
            .OrderBy(c => c.Nome)
            .ToListAsync();
    }

    public async Task<Cargo?> GetCargoById(int id)
    {
        return await context.Cargos
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Cargo?> GetCargoByNome(string nome)
    {
        return await context.Cargos
            .FirstOrDefaultAsync(c => c.Nome.ToLower() == nome.ToLower());
    }

    public async Task CreateCargo(Cargo cargo)
    {
        cargo.InseridoEm = DateTime.UtcNow;
        await context.Cargos.AddAsync(cargo);
        await context.SaveChangesAsync();
    }

    public async Task UpdateCargo(Cargo cargo)
    {
        context.Cargos.Update(cargo);
        await context.SaveChangesAsync();
    }

    public async Task DeleteCargo(int id)
    {
        var cargo = await GetCargoById(id);
        if (cargo != null)
        {
            context.Cargos.Remove(cargo);
            await context.SaveChangesAsync();
        }
    }
}
