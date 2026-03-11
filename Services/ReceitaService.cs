using Byte_Backend.Entidades;
using Byte_Backend.Interfaces;

namespace Byte_Backend.Services;

public class ReceitaService
{
    private readonly IReceitaRepository receitaRepository;
    private readonly IProdutoRepository produtoRepository;

    public ReceitaService(IReceitaRepository receitaRepository, IProdutoRepository produtoRepository)
    {
        this.receitaRepository = receitaRepository;
        this.produtoRepository = produtoRepository;
    }

    public async Task<IEnumerable<Receita>> GetAllReceitas()
    {
        return await receitaRepository.GetAllReceitas();
    }

    public async Task<Receita?> GetReceitaById(int id)
    {
        return await receitaRepository.GetReceitaById(id);
    }

    public async Task<Receita?> GetReceitaByProdutoId(int produtoId)
    {
        return await receitaRepository.GetReceitaByProdutoId(produtoId);
    }

    public async Task<(bool Success, string Message)> CreateReceita(Receita receita)
    {
        var produto = await produtoRepository.GetProdutoById(receita.ProdutoId);
        if (produto == null)
        {
            return (false, "Produto não encontrado.");
        }

        var receitaExistente = await receitaRepository.GetReceitaByProdutoId(receita.ProdutoId);
        if (receitaExistente != null)
        {
            return (false, "Este produto já possui uma receita cadastrada.");
        }

        await receitaRepository.CreateReceita(receita);
        return (true, "Receita criada com sucesso.");
    }

    public async Task<(bool Success, string Message)> UpdateReceita(int id, Receita receita)
    {
        var receitaExistente = await receitaRepository.GetReceitaById(id);
        if (receitaExistente == null)
        {
            return (false, "Receita não encontrada.");
        }

        receitaExistente.Descricao = receita.Descricao;
        receitaExistente.TempoPreparo = receita.TempoPreparo;
        receitaExistente.ModificadoEm = DateTime.Now;
        receitaExistente.ModificadoPor = receita.ModificadoPor;

        await receitaRepository.UpdateReceita(receitaExistente);
        return (true, "Receita atualizada com sucesso.");
    }

    public async Task<(bool Success, string Message)> DeleteReceita(int id)
    {
        var receita = await receitaRepository.GetReceitaById(id);
        if (receita == null)
        {
            return (false, "Receita não encontrada.");
        }

        await receitaRepository.DeleteReceita(id);
        return (true, "Receita excluída com sucesso.");
    }
}
