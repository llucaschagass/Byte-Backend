using Byte_Backend.Entidades;
using Byte_Backend.Interfaces;

namespace Byte_Backend.Services;

public class ClienteService
{
    private readonly IClienteRepository clienteRepository;
    private readonly IPessoaRepository pessoaRepository;

    public ClienteService(IClienteRepository clienteRepository, IPessoaRepository pessoaRepository)
    {
        this.clienteRepository = clienteRepository;
        this.pessoaRepository = pessoaRepository;
    }

    public async Task<IEnumerable<Cliente>> GetAllClientes()
    {
        return await clienteRepository.GetAllClientes();
    }

    public async Task<Cliente?> GetClienteById(int id)
    {
        return await clienteRepository.GetClienteById(id);
    }

    public async Task<(bool Success, string Message)> CreateCliente(Cliente cliente)
    {
        // Verificar se a pessoa existe
        var pessoa = await pessoaRepository.GetPessoaById(cliente.PessoaId);
        if (pessoa == null)
        {
            return (false, "Pessoa não encontrada.");
        }

        // Verificar se já existe um cliente para essa pessoa
        var clienteExistente = await clienteRepository.GetClienteByPessoaId(cliente.PessoaId);
        if (clienteExistente != null)
        {
            return (false, $"Já existe um cliente cadastrado para a pessoa '{pessoa.Nome}'.");
        }

        await clienteRepository.CreateCliente(cliente);
        return (true, "Cliente criado com sucesso.");
    }

    public async Task<(bool Success, string Message)> UpdateCliente(int id, Cliente cliente)
    {
        var clienteExistente = await clienteRepository.GetClienteById(id);
        if (clienteExistente == null)
        {
            return (false, "Cliente não encontrado.");
        }

        // Verificar se a pessoa existe
        var pessoa = await pessoaRepository.GetPessoaById(cliente.PessoaId);
        if (pessoa == null)
        {
            return (false, "Pessoa não encontrada.");
        }

        // Verificar se já existe outro cliente para essa pessoa
        var clienteComMesmaPessoa = await clienteRepository.GetClienteByPessoaId(cliente.PessoaId);
        if (clienteComMesmaPessoa != null && clienteComMesmaPessoa.Id != id)
        {
            return (false, $"Já existe outro cliente cadastrado para a pessoa '{pessoa.Nome}'.");
        }

        clienteExistente.PessoaId = cliente.PessoaId;
        clienteExistente.PontosFidelidade = cliente.PontosFidelidade;

        await clienteRepository.UpdateCliente(clienteExistente);
        return (true, "Cliente atualizado com sucesso.");
    }

    public async Task<(bool Success, string Message)> DeleteCliente(int id)
    {
        var cliente = await clienteRepository.GetClienteById(id);
        if (cliente == null)
        {
            return (false, "Cliente não encontrado.");
        }

        await clienteRepository.DeleteCliente(id);
        return (true, "Cliente excluído com sucesso.");
    }
}
