using Byte_Backend.Entidades;
using Byte_Backend.Interfaces;

namespace Byte_Backend.Services;

public class UsuarioService
{
    private readonly IUsuarioRepository usuarioRepository;
    private readonly IFuncionarioRepository funcionarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository, IFuncionarioRepository funcionarioRepository)
    {
        this.usuarioRepository = usuarioRepository;
        this.funcionarioRepository = funcionarioRepository;
    }

    public async Task<IEnumerable<Usuario>> GetAllUsuarios()
    {
        return await usuarioRepository.GetAllUsuarios();
    }

    public async Task<Usuario?> GetUsuarioById(int id)
    {
        return await usuarioRepository.GetUsuarioById(id);
    }

    public async Task<(bool Success, string Message)> CreateUsuario(Usuario usuario)
    {
        // Verificar se o funcionário existe
        var funcionario = await funcionarioRepository.GetFuncionarioById(usuario.FuncionarioId);
        if (funcionario == null)
        {
            return (false, "Funcionário não encontrado.");
        }

        // Verificar duplicidade de Login
        var usuarioExistente = await usuarioRepository.GetUsuarioByLogin(usuario.Login);
        if (usuarioExistente != null)
        {
            return (false, $"Já existe um usuário cadastrado com o login '{usuario.Login}'.");
        }

        await usuarioRepository.CreateUsuario(usuario);
        return (true, "Usuário criado com sucesso.");
    }

    public async Task<(bool Success, string Message)> UpdateUsuario(int id, Usuario usuario)
    {
        var usuarioExistente = await usuarioRepository.GetUsuarioById(id);
        if (usuarioExistente == null)
        {
            return (false, "Usuário não encontrado.");
        }

        // Verificar se o funcionário existe
        var funcionario = await funcionarioRepository.GetFuncionarioById(usuario.FuncionarioId);
        if (funcionario == null)
        {
            return (false, "Funcionário não encontrado.");
        }

        // Verificar se o novo login já existe em outro usuário
        var usuarioComMesmoLogin = await usuarioRepository.GetUsuarioByLogin(usuario.Login);
        if (usuarioComMesmoLogin != null && usuarioComMesmoLogin.Id != id)
        {
            return (false, $"Já existe outro usuário cadastrado com o login '{usuario.Login}'.");
        }

        usuarioExistente.FuncionarioId = usuario.FuncionarioId;
        usuarioExistente.Login = usuario.Login;
        usuarioExistente.SenhaHash = usuario.SenhaHash;

        await usuarioRepository.UpdateUsuario(usuarioExistente);
        return (true, "Usuário atualizado com sucesso.");
    }

    public async Task<(bool Success, string Message)> DeleteUsuario(int id)
    {
        var usuario = await usuarioRepository.GetUsuarioById(id);
        if (usuario == null)
        {
            return (false, "Usuário não encontrado.");
        }

        await usuarioRepository.DeleteUsuario(id);
        return (true, "Usuário excluído com sucesso.");
    }
}
