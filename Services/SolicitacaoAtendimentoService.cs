using Byte_Backend.Entidades;
using Byte_Backend.Interfaces;

namespace Byte_Backend.Services;

public class SolicitacaoAtendimentoService
{
    private readonly ISolicitacaoAtendimentoRepository solicitacaoRepository;
    private readonly IUsuarioRepository usuarioRepository;

    public SolicitacaoAtendimentoService(
        ISolicitacaoAtendimentoRepository solicitacaoRepository,
        IUsuarioRepository usuarioRepository)
    {
        this.solicitacaoRepository = solicitacaoRepository;
        this.usuarioRepository = usuarioRepository;
    }

    public async Task<IEnumerable<SolicitacaoAtendimento>> GetAll()
    {
        return await solicitacaoRepository.GetAll();
    }

    public async Task<SolicitacaoAtendimento?> GetById(int id)
    {
        return await solicitacaoRepository.GetById(id);
    }

    public async Task<IEnumerable<SolicitacaoAtendimento>> GetPendentes()
    {
        return await solicitacaoRepository.GetPendentes();
    }

    public async Task<(bool Success, string Message, SolicitacaoAtendimento? Solicitacao)> CriarSolicitacao(int numeroMesa)
    {
        if (numeroMesa <= 0)
        {
            return (false, "O número da mesa deve ser maior que zero.", null);
        }

        var solicitacao = new SolicitacaoAtendimento
        {
            NumeroMesa = numeroMesa,
            Atendida = "N"
        };

        await solicitacaoRepository.Create(solicitacao);
        return (true, "Garçom chamado com sucesso.", solicitacao);
    }

    public async Task<(bool Success, string Message)> AtenderSolicitacao(int id, int usuarioId)
    {
        var solicitacao = await solicitacaoRepository.GetById(id);
        if (solicitacao == null)
        {
            return (false, "Solicitação de atendimento não encontrada.");
        }

        if (solicitacao.Atendida == "S")
        {
            return (false, "Esta solicitação já foi atendida.");
        }

        var usuario = await usuarioRepository.GetUsuarioById(usuarioId);
        if (usuario == null)
        {
            return (false, "Usuário não encontrado.");
        }

        solicitacao.Atendida = "S";
        solicitacao.AtendidoPorId = usuarioId;
        solicitacao.AtendidoEm = DateTime.UtcNow;

        await solicitacaoRepository.Update(solicitacao);
        return (true, "Atendimento registrado com sucesso.");
    }
}
