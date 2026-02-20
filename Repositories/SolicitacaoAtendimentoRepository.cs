using Byte_Backend.Dados;
using Byte_Backend.Entidades;
using Byte_Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Byte_Backend.Repositories;

public class SolicitacaoAtendimentoRepository : ISolicitacaoAtendimentoRepository
{
    private readonly ByteDbContext context;

    public SolicitacaoAtendimentoRepository(ByteDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<SolicitacaoAtendimento>> GetAll()
    {
        return await context.SolicitacoesAtendimento
            .Include(s => s.AtendidoPor)
                .ThenInclude(u => u!.Funcionario)
                    .ThenInclude(f => f!.Pessoa)
            .OrderByDescending(s => s.InseridoEm)
            .ToListAsync();
    }

    public async Task<SolicitacaoAtendimento?> GetById(int id)
    {
        return await context.SolicitacoesAtendimento
            .Include(s => s.AtendidoPor)
                .ThenInclude(u => u!.Funcionario)
                    .ThenInclude(f => f!.Pessoa)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<SolicitacaoAtendimento>> GetPendentes()
    {
        return await context.SolicitacoesAtendimento
            .Where(s => s.Atendida == "N")
            .OrderBy(s => s.InseridoEm)
            .ToListAsync();
    }

    public async Task Create(SolicitacaoAtendimento solicitacao)
    {
        solicitacao.InseridoEm = DateTime.UtcNow;
        await context.SolicitacoesAtendimento.AddAsync(solicitacao);
        await context.SaveChangesAsync();
    }

    public async Task Update(SolicitacaoAtendimento solicitacao)
    {
        context.SolicitacoesAtendimento.Update(solicitacao);
        await context.SaveChangesAsync();
    }
}
