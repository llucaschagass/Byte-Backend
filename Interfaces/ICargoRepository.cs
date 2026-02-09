using Byte_Backend.Entidades;

namespace Byte_Backend.Interfaces;

public interface ICargoRepository
{
    Task<IEnumerable<Cargo>> GetAllCargos();
    Task<Cargo?> GetCargoById(int id);
    Task<Cargo?> GetCargoByNome(string nome);
    Task CreateCargo(Cargo cargo);
    Task UpdateCargo(Cargo cargo);
    Task DeleteCargo(int id);
}
