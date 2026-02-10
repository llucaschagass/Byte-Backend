using Byte_Backend.Entidades;

namespace Byte_Backend.Interfaces;

public interface IClienteRepository
{
    Task<IEnumerable<Cliente>> GetAllClientes();
    Task<Cliente?> GetClienteById(int id);
    Task<Cliente?> GetClienteByPessoaId(int pessoaId);
    Task CreateCliente(Cliente cliente);
    Task UpdateCliente(Cliente cliente);
    Task DeleteCliente(int id);
}
