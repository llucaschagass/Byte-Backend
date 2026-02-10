using Byte_Backend.Entidades;
using Byte_Backend.Interfaces;

namespace Byte_Backend.Services;

public class FuncionarioService
{
    private readonly IFuncionarioRepository funcionarioRepository;
    private readonly IPessoaRepository pessoaRepository;
    private readonly ICargoRepository cargoRepository;

    public FuncionarioService(
        IFuncionarioRepository funcionarioRepository,
        IPessoaRepository pessoaRepository,
        ICargoRepository cargoRepository)
    {
        this.funcionarioRepository = funcionarioRepository;
        this.pessoaRepository = pessoaRepository;
        this.cargoRepository = cargoRepository;
    }

    public async Task<IEnumerable<Funcionario>> GetAllFuncionarios()
    {
        return await funcionarioRepository.GetAllFuncionarios();
    }

    public async Task<Funcionario?> GetFuncionarioById(int id)
    {
        return await funcionarioRepository.GetFuncionarioById(id);
    }

    public async Task<(bool Success, string Message)> CreateFuncionario(Funcionario funcionario)
    {
        // Verificar se a pessoa existe
        var pessoa = await pessoaRepository.GetPessoaById(funcionario.PessoaId);
        if (pessoa == null)
        {
            return (false, "Pessoa não encontrada.");
        }

        // Verificar se o cargo existe
        var cargo = await cargoRepository.GetCargoById(funcionario.CargoId);
        if (cargo == null)
        {
            return (false, "Cargo não encontrado.");
        }

        await funcionarioRepository.CreateFuncionario(funcionario);
        return (true, "Funcionário criado com sucesso.");
    }

    public async Task<(bool Success, string Message)> UpdateFuncionario(int id, Funcionario funcionario)
    {
        var funcionarioExistente = await funcionarioRepository.GetFuncionarioById(id);
        if (funcionarioExistente == null)
        {
            return (false, "Funcionário não encontrado.");
        }

        // Verificar se a pessoa existe
        var pessoa = await pessoaRepository.GetPessoaById(funcionario.PessoaId);
        if (pessoa == null)
        {
            return (false, "Pessoa não encontrada.");
        }

        // Verificar se o cargo existe
        var cargo = await cargoRepository.GetCargoById(funcionario.CargoId);
        if (cargo == null)
        {
            return (false, "Cargo não encontrado.");
        }

        funcionarioExistente.PessoaId = funcionario.PessoaId;
        funcionarioExistente.CargoId = funcionario.CargoId;
        funcionarioExistente.Ativo = funcionario.Ativo;

        await funcionarioRepository.UpdateFuncionario(funcionarioExistente);
        return (true, "Funcionário atualizado com sucesso.");
    }

    public async Task<(bool Success, string Message)> DeleteFuncionario(int id)
    {
        var funcionario = await funcionarioRepository.GetFuncionarioById(id);
        if (funcionario == null)
        {
            return (false, "Funcionário não encontrado.");
        }

        await funcionarioRepository.DeleteFuncionario(id);
        return (true, "Funcionário excluído com sucesso.");
    }
}
