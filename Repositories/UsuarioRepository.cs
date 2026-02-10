using Byte_Backend.Dados;
using Byte_Backend.Entidades;
using Byte_Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Byte_Backend.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly ByteDbContext context;

    public UsuarioRepository(ByteDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<Usuario>> GetAllUsuarios()
    {
        return await context.Usuarios
            .Include(u => u.Funcionario)
                .ThenInclude(f => f!.Pessoa)
            .Include(u => u.Funcionario)
                .ThenInclude(f => f!.Cargo)
            .OrderBy(u => u.Login)
            .ToListAsync();
    }

    public async Task<Usuario?> GetUsuarioById(int id)
    {
        return await context.Usuarios
            .Include(u => u.Funcionario)
                .ThenInclude(f => f!.Pessoa)
            .Include(u => u.Funcionario)
                .ThenInclude(f => f!.Cargo)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<Usuario?> GetUsuarioByLogin(string login)
    {
        return await context.Usuarios
            .Include(u => u.Funcionario)
                .ThenInclude(f => f!.Pessoa)
            .Include(u => u.Funcionario)
                .ThenInclude(f => f!.Cargo)
            .FirstOrDefaultAsync(u => u.Login == login);
    }

    public async Task CreateUsuario(Usuario usuario)
    {
        usuario.InseridoEm = DateTime.UtcNow;
        await context.Usuarios.AddAsync(usuario);
        await context.SaveChangesAsync();
    }

    public async Task UpdateUsuario(Usuario usuario)
    {
        context.Usuarios.Update(usuario);
        await context.SaveChangesAsync();
    }

    public async Task DeleteUsuario(int id)
    {
        var usuario = await GetUsuarioById(id);
        if (usuario != null)
        {
            context.Usuarios.Remove(usuario);
            await context.SaveChangesAsync();
        }
    }
}
