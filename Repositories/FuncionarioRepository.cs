using Byte_Backend.Dados;
using Byte_Backend.Entidades;
using Byte_Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Byte_Backend.Repositories;

public class FuncionarioRepository : IFuncionarioRepository
{
    private readonly ByteDbContext context;

    public FuncionarioRepository(ByteDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<Funcionario>> GetAllFuncionarios()
    {
        return await context.Funcionarios
            .Include(f => f.Pessoa)
            .Include(f => f.Cargo)
            .OrderBy(f => f.Pessoa!.Nome)
            .ToListAsync();
    }

    public async Task<Funcionario?> GetFuncionarioById(int id)
    {
        return await context.Funcionarios
            .Include(f => f.Pessoa)
            .Include(f => f.Cargo)
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<Funcionario?> GetFuncionarioByCodigo(string codigo)
    {
        return await context.Funcionarios
            .Include(f => f.Pessoa)
            .Include(f => f.Cargo)
            .FirstOrDefaultAsync(f => f.CodigoFuncionario == codigo);
    }

    public async Task CreateFuncionario(Funcionario funcionario)
    {
        funcionario.InseridoEm = DateTime.UtcNow;
        funcionario.Ativo = true;
        
        // Gerar código automático baseado no próximo ID
        var ultimoFuncionario = await context.Funcionarios
            .OrderByDescending(f => f.Id)
            .FirstOrDefaultAsync();
        
        var proximoNumero = (ultimoFuncionario?.Id ?? 0) + 1;
        funcionario.CodigoFuncionario = $"FUNC{proximoNumero:D4}";
        
        await context.Funcionarios.AddAsync(funcionario);
        await context.SaveChangesAsync();
    }

    public async Task UpdateFuncionario(Funcionario funcionario)
    {
        context.Funcionarios.Update(funcionario);
        await context.SaveChangesAsync();
    }

    public async Task DeleteFuncionario(int id)
    {
        var funcionario = await context.Funcionarios.FindAsync(id);
        if (funcionario != null)
        {
            context.Funcionarios.Remove(funcionario);
            await context.SaveChangesAsync();
        }
    }
}
