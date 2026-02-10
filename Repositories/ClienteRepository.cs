using Byte_Backend.Dados;
using Byte_Backend.Entidades;
using Byte_Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Byte_Backend.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly ByteDbContext context;

    public ClienteRepository(ByteDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<Cliente>> GetAllClientes()
    {
        return await context.Clientes
            .Include(c => c.Pessoa)
            .OrderBy(c => c.Pessoa!.Nome)
            .ToListAsync();
    }

    public async Task<Cliente?> GetClienteById(int id)
    {
        return await context.Clientes
            .Include(c => c.Pessoa)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Cliente?> GetClienteByPessoaId(int pessoaId)
    {
        return await context.Clientes
            .Include(c => c.Pessoa)
            .FirstOrDefaultAsync(c => c.PessoaId == pessoaId);
    }

    public async Task CreateCliente(Cliente cliente)
    {
        cliente.InseridoEm = DateTime.UtcNow;
        await context.Clientes.AddAsync(cliente);
        await context.SaveChangesAsync();
    }

    public async Task UpdateCliente(Cliente cliente)
    {
        context.Clientes.Update(cliente);
        await context.SaveChangesAsync();
    }

    public async Task DeleteCliente(int id)
    {
        var cliente = await GetClienteById(id);
        if (cliente != null)
        {
            context.Clientes.Remove(cliente);
            await context.SaveChangesAsync();
        }
    }
}
