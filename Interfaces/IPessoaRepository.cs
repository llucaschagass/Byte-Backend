using Byte_Backend.Entidades;

namespace Byte_Backend.Interfaces;

public interface IPessoaRepository
{
    Task<IEnumerable<Pessoa>> GetAllPessoas();
    Task<Pessoa?> GetPessoaById(int id);
    Task<Pessoa?> GetPessoaByCpf(string cpf);
    Task CreatePessoa(Pessoa pessoa);
    Task UpdatePessoa(Pessoa pessoa);
    Task DeletePessoa(int id);
}
