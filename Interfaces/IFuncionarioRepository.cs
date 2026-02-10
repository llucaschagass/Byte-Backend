using Byte_Backend.Entidades;

namespace Byte_Backend.Interfaces;

public interface IFuncionarioRepository
{
    Task<IEnumerable<Funcionario>> GetAllFuncionarios();
    Task<Funcionario?> GetFuncionarioById(int id);
    Task<Funcionario?> GetFuncionarioByCodigo(string codigo);
    Task CreateFuncionario(Funcionario funcionario);
    Task UpdateFuncionario(Funcionario funcionario);
    Task DeleteFuncionario(int id);
}
