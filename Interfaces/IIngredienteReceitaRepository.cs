using Byte_Backend.Entidades;

namespace Byte_Backend.Interfaces;

public interface IIngredienteReceitaRepository
{
    Task<IEnumerable<IngredienteReceita>> GetByReceitaId(int receitaId);
    Task<IngredienteReceita?> GetById(int id);
    Task Create(IngredienteReceita ingrediente);
    Task Update(IngredienteReceita ingrediente);
    Task Delete(int id);
}
