using Byte_Backend.Entidades;

namespace Byte_Backend.Interfaces;

public interface IReceitaRepository
{
    Task<IEnumerable<Receita>> GetAllReceitas();
    Task<Receita?> GetReceitaById(int id);
    Task<Receita?> GetReceitaByProdutoId(int produtoId);
    Task CreateReceita(Receita receita);
    Task UpdateReceita(Receita receita);
    Task DeleteReceita(int id);
}
