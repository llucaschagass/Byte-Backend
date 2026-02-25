using Byte_Backend.Entidades;
using Byte_Backend.Interfaces;

namespace Byte_Backend.Services;

public class UsuarioPermissaoService
{
    private readonly IUsuarioPermissaoRepository usuarioPermissaoRepository;
    private readonly IUsuarioRepository usuarioRepository;

    public UsuarioPermissaoService(
        IUsuarioPermissaoRepository usuarioPermissaoRepository,
        IUsuarioRepository usuarioRepository)
    {
        this.usuarioPermissaoRepository = usuarioPermissaoRepository;
        this.usuarioRepository = usuarioRepository;
    }

    public async Task<IEnumerable<UsuarioPermissao>> GetAll()
    {
        return await usuarioPermissaoRepository.GetAll();
    }

    public async Task<UsuarioPermissao?> GetById(int id)
    {
        return await usuarioPermissaoRepository.GetById(id);
    }

    public async Task<UsuarioPermissao?> GetByUsuarioId(int usuarioId)
    {
        return await usuarioPermissaoRepository.GetByUsuarioId(usuarioId);
    }

    public async Task<(bool Success, string Message, UsuarioPermissao? Permissao)> Create(
        int usuarioId, bool garcom, bool cozinha, bool gerencia, PermissaoPrincipal principal)
    {
        var usuario = await usuarioRepository.GetUsuarioById(usuarioId);
        if (usuario == null)
            return (false, "Usuário não encontrado.", null);

        var existente = await usuarioPermissaoRepository.GetByUsuarioId(usuarioId);
        if (existente != null)
            return (false, "Este usuário já possui permissões cadastradas.", null);

        var validacao = ValidarPermissaoPrincipal(garcom, cozinha, gerencia, principal);
        if (!validacao.Success)
            return (false, validacao.Message, null);

        var permissao = new UsuarioPermissao
        {
            UsuarioId = usuarioId,
            Garcom = garcom,
            Cozinha = cozinha,
            Gerencia = gerencia,
            Principal = principal
        };

        await usuarioPermissaoRepository.Create(permissao);
        return (true, "Permissões cadastradas com sucesso.", permissao);
    }

    public async Task<(bool Success, string Message)> Update(
        int id, bool garcom, bool cozinha, bool gerencia, PermissaoPrincipal principal)
    {
        var permissao = await usuarioPermissaoRepository.GetById(id);
        if (permissao == null)
            return (false, "Permissão não encontrada.");

        var validacao = ValidarPermissaoPrincipal(garcom, cozinha, gerencia, principal);
        if (!validacao.Success)
            return (false, validacao.Message);

        permissao.Garcom = garcom;
        permissao.Cozinha = cozinha;
        permissao.Gerencia = gerencia;
        permissao.Principal = principal;

        await usuarioPermissaoRepository.Update(permissao);
        return (true, "Permissões atualizadas com sucesso.");
    }

    public async Task<(bool Success, string Message)> Delete(int id)
    {
        var permissao = await usuarioPermissaoRepository.GetById(id);
        if (permissao == null)
            return (false, "Permissão não encontrada.");

        await usuarioPermissaoRepository.Delete(permissao);
        return (true, "Permissões removidas com sucesso.");
    }

    private static (bool Success, string Message) ValidarPermissaoPrincipal(
        bool garcom, bool cozinha, bool gerencia, PermissaoPrincipal principal)
    {
        if (principal == PermissaoPrincipal.G && !garcom)
            return (false, "A permissão principal é Garçom, mas o acesso de Garçom está desativado.");

        if (principal == PermissaoPrincipal.C && !cozinha)
            return (false, "A permissão principal é Cozinha, mas o acesso de Cozinha está desativado.");

        if (principal == PermissaoPrincipal.P && !gerencia)
            return (false, "A permissão principal é Gerência, mas o acesso de Gerência está desativado.");

        return (true, string.Empty);
    }
}
