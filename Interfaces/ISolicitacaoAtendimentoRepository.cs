using Byte_Backend.Entidades;

namespace Byte_Backend.Interfaces;

public interface ISolicitacaoAtendimentoRepository
{
    Task<IEnumerable<SolicitacaoAtendimento>> GetAll();
    Task<SolicitacaoAtendimento?> GetById(int id);
    Task<IEnumerable<SolicitacaoAtendimento>> GetPendentes();
    Task Create(SolicitacaoAtendimento solicitacao);
    Task Update(SolicitacaoAtendimento solicitacao);
}
