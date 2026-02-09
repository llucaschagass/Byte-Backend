using Byte_Backend.Dados;
using Byte_Backend.Entidades;
using Byte_Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Byte_Backend.Repositories;

public class PessoaRepository : IPessoaRepository
{
    private readonly ByteDbContext context;

    public PessoaRepository(ByteDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<Pessoa>> GetAllPessoas()
    {
        return await context.Pessoas
            .OrderBy(p => p.Nome)
            .ToListAsync();
    }

    public async Task<Pessoa?> GetPessoaById(int id)
    {
        return await context.Pessoas
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Pessoa?> GetPessoaByCpf(string cpf)
    {
        return await context.Pessoas
            .FirstOrDefaultAsync(p => p.CPF == cpf);
    }

    public async Task CreatePessoa(Pessoa pessoa)
    {
        pessoa.InseridoEm = DateTime.UtcNow;
        await context.Pessoas.AddAsync(pessoa);
        await context.SaveChangesAsync();
    }

    public async Task UpdatePessoa(Pessoa pessoa)
    {
        context.Pessoas.Update(pessoa);
        await context.SaveChangesAsync();
    }

    public async Task DeletePessoa(int id)
    {
        var pessoa = await GetPessoaById(id);
        if (pessoa != null)
        {
            context.Pessoas.Remove(pessoa);
            await context.SaveChangesAsync();
        }
    }
}
