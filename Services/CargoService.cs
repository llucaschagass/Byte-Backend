using Byte_Backend.Entidades;
using Byte_Backend.Interfaces;

namespace Byte_Backend.Services;

public class CargoService
{
    private readonly ICargoRepository cargoRepository;

    public CargoService(ICargoRepository cargoRepository)
    {
        this.cargoRepository = cargoRepository;
    }

    public async Task<IEnumerable<Cargo>> GetAllCargos()
    {
        return await cargoRepository.GetAllCargos();
    }

    public async Task<Cargo?> GetCargoById(int id)
    {
        return await cargoRepository.GetCargoById(id);
    }

    public async Task<(bool Success, string Message)> CreateCargo(Cargo cargo)
    {
        // Verificar duplicidade de nome
        var cargoExistente = await cargoRepository.GetCargoByNome(cargo.Nome);
        if (cargoExistente != null)
        {
            return (false, $"Já existe um cargo com o nome '{cargo.Nome}'.");
        }

        await cargoRepository.CreateCargo(cargo);
        return (true, "Cargo criado com sucesso.");
    }

    public async Task<(bool Success, string Message)> UpdateCargo(int id, Cargo cargo)
    {
        var cargoExistente = await cargoRepository.GetCargoById(id);
        if (cargoExistente == null)
        {
            return (false, "Cargo não encontrado.");
        }

        // Verificar se o novo nome já existe em outro cargo
        var cargoComMesmoNome = await cargoRepository.GetCargoByNome(cargo.Nome);
        if (cargoComMesmoNome != null && cargoComMesmoNome.Id != id)
        {
            return (false, $"Já existe outro cargo com o nome '{cargo.Nome}'.");
        }

        cargoExistente.Nome = cargo.Nome;
        cargoExistente.Descricao = cargo.Descricao;

        await cargoRepository.UpdateCargo(cargoExistente);
        return (true, "Cargo atualizado com sucesso.");
    }

    public async Task<(bool Success, string Message)> DeleteCargo(int id)
    {
        var cargo = await cargoRepository.GetCargoById(id);
        if (cargo == null)
        {
            return (false, "Cargo não encontrado.");
        }

        await cargoRepository.DeleteCargo(id);
        return (true, "Cargo excluído com sucesso.");
    }
}
