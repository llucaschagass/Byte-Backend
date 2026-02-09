using Byte_Backend.Entidades;
using Byte_Backend.Interfaces;

namespace Byte_Backend.Services;

public class PessoaService
{
    private readonly IPessoaRepository pessoaRepository;

    public PessoaService(IPessoaRepository pessoaRepository)
    {
        this.pessoaRepository = pessoaRepository;
    }

    public async Task<IEnumerable<Pessoa>> GetAllPessoas()
    {
        return await pessoaRepository.GetAllPessoas();
    }

    public async Task<Pessoa?> GetPessoaById(int id)
    {
        return await pessoaRepository.GetPessoaById(id);
    }

    public async Task<(bool Success, string Message)> CreatePessoa(Pessoa pessoa)
    {
        // Verificar duplicidade de CPF
        if (!string.IsNullOrEmpty(pessoa.CPF))
        {
            var pessoaExistente = await pessoaRepository.GetPessoaByCpf(pessoa.CPF);
            if (pessoaExistente != null)
            {
                return (false, $"Já existe uma pessoa cadastrada com o CPF '{pessoa.CPF}'.");
            }
        }

        await pessoaRepository.CreatePessoa(pessoa);
        return (true, "Pessoa criada com sucesso.");
    }

    public async Task<(bool Success, string Message)> UpdatePessoa(int id, Pessoa pessoa)
    {
        var pessoaExistente = await pessoaRepository.GetPessoaById(id);
        if (pessoaExistente == null)
        {
            return (false, "Pessoa não encontrada.");
        }

        // Verificar se o novo CPF já existe em outra pessoa
        if (!string.IsNullOrEmpty(pessoa.CPF))
        {
            var pessoaComMesmoCpf = await pessoaRepository.GetPessoaByCpf(pessoa.CPF);
            if (pessoaComMesmoCpf != null && pessoaComMesmoCpf.Id != id)
            {
                return (false, $"Já existe outra pessoa cadastrada com o CPF '{pessoa.CPF}'.");
            }
        }

        pessoaExistente.Nome = pessoa.Nome;
        pessoaExistente.Email = pessoa.Email;
        pessoaExistente.CPF = pessoa.CPF;
        pessoaExistente.Telefone = pessoa.Telefone;

        await pessoaRepository.UpdatePessoa(pessoaExistente);
        return (true, "Pessoa atualizada com sucesso.");
    }

    public async Task<(bool Success, string Message)> DeletePessoa(int id)
    {
        var pessoa = await pessoaRepository.GetPessoaById(id);
        if (pessoa == null)
        {
            return (false, "Pessoa não encontrada.");
        }

        await pessoaRepository.DeletePessoa(id);
        return (true, "Pessoa excluída com sucesso.");
    }
}
