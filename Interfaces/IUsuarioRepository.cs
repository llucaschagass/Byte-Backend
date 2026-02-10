using Byte_Backend.Entidades;

namespace Byte_Backend.Interfaces;

public interface IUsuarioRepository
{
    Task<IEnumerable<Usuario>> GetAllUsuarios();
    Task<Usuario?> GetUsuarioById(int id);
    Task<Usuario?> GetUsuarioByLogin(string login);
    Task CreateUsuario(Usuario usuario);
    Task UpdateUsuario(Usuario usuario);
    Task DeleteUsuario(int id);
}
