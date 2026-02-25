using Byte_Backend.Entidades;

namespace Byte_Backend.Interfaces;

public interface IUsuarioPermissaoRepository
{
    Task<IEnumerable<UsuarioPermissao>> GetAll();
    Task<UsuarioPermissao?> GetById(int id);
    Task<UsuarioPermissao?> GetByUsuarioId(int usuarioId);
    Task Create(UsuarioPermissao usuarioPermissao);
    Task Update(UsuarioPermissao usuarioPermissao);
    Task Delete(UsuarioPermissao usuarioPermissao);
}
