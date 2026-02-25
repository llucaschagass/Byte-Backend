using Byte_Backend.Dados;
using Byte_Backend.Entidades;
using Byte_Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Byte_Backend.Repositories;

public class UsuarioPermissaoRepository : IUsuarioPermissaoRepository
{
    private readonly ByteDbContext context;

    public UsuarioPermissaoRepository(ByteDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<UsuarioPermissao>> GetAll()
    {
        return await context.UsuariosPermissoes
            .Include(up => up.Usuario)
                .ThenInclude(u => u!.Funcionario)
                    .ThenInclude(f => f!.Pessoa)
            .OrderByDescending(up => up.InseridoEm)
            .ToListAsync();
    }

    public async Task<UsuarioPermissao?> GetById(int id)
    {
        return await context.UsuariosPermissoes
            .Include(up => up.Usuario)
                .ThenInclude(u => u!.Funcionario)
                    .ThenInclude(f => f!.Pessoa)
            .FirstOrDefaultAsync(up => up.Id == id);
    }

    public async Task<UsuarioPermissao?> GetByUsuarioId(int usuarioId)
    {
        return await context.UsuariosPermissoes
            .Include(up => up.Usuario)
                .ThenInclude(u => u!.Funcionario)
                    .ThenInclude(f => f!.Pessoa)
            .FirstOrDefaultAsync(up => up.UsuarioId == usuarioId);
    }

    public async Task Create(UsuarioPermissao usuarioPermissao)
    {
        usuarioPermissao.InseridoEm = DateTime.UtcNow;
        await context.UsuariosPermissoes.AddAsync(usuarioPermissao);
        await context.SaveChangesAsync();
    }

    public async Task Update(UsuarioPermissao usuarioPermissao)
    {
        context.UsuariosPermissoes.Update(usuarioPermissao);
        await context.SaveChangesAsync();
    }

    public async Task Delete(UsuarioPermissao usuarioPermissao)
    {
        context.UsuariosPermissoes.Remove(usuarioPermissao);
        await context.SaveChangesAsync();
    }
}
