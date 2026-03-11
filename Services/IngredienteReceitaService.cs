using Byte_Backend.Entidades;
using Byte_Backend.Interfaces;

namespace Byte_Backend.Services;

public class IngredienteReceitaService
{
    private readonly IIngredienteReceitaRepository ingredienteRepository;
    private readonly IReceitaRepository receitaRepository;

    public IngredienteReceitaService(IIngredienteReceitaRepository ingredienteRepository, IReceitaRepository receitaRepository)
    {
        this.ingredienteRepository = ingredienteRepository;
        this.receitaRepository = receitaRepository;
    }

    public async Task<IEnumerable<IngredienteReceita>> GetByReceitaId(int receitaId)
    {
        return await ingredienteRepository.GetByReceitaId(receitaId);
    }

    public async Task<IngredienteReceita?> GetById(int id)
    {
        return await ingredienteRepository.GetById(id);
    }

    public async Task<(bool Success, string Message)> Create(IngredienteReceita ingrediente)
    {
        var receita = await receitaRepository.GetReceitaById(ingrediente.ReceitaId);
        if (receita == null)
        {
            return (false, "Receita não encontrada.");
        }

        await ingredienteRepository.Create(ingrediente);
        return (true, "Ingrediente adicionado com sucesso.");
    }

    public async Task<(bool Success, string Message)> Update(int id, IngredienteReceita ingrediente)
    {
        var existente = await ingredienteRepository.GetById(id);
        if (existente == null)
        {
            return (false, "Ingrediente não encontrado.");
        }

        existente.Nome = ingrediente.Nome;
        existente.Quantidade = ingrediente.Quantidade;
        existente.Observacao = ingrediente.Observacao;
        existente.ModificadoEm = DateTime.Now;
        existente.ModificadoPor = ingrediente.ModificadoPor;

        await ingredienteRepository.Update(existente);
        return (true, "Ingrediente atualizado com sucesso.");
    }

    public async Task<(bool Success, string Message)> Delete(int id)
    {
        var ingrediente = await ingredienteRepository.GetById(id);
        if (ingrediente == null)
        {
            return (false, "Ingrediente não encontrado.");
        }

        await ingredienteRepository.Delete(id);
        return (true, "Ingrediente excluído com sucesso.");
    }
}
