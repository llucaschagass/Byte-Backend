using Byte_Backend.Entidades;

namespace Byte_Backend.Interfaces;

public interface ICategoriaRepository
{
    Task<IEnumerable<Categoria>> GetAllCategorias();
    Task<Categoria?> GetCategoriaById(int id);
    Task<Categoria?> GetCategoriaByNome(string nome);
    Task CreateCategoria(Categoria categoria);
    Task UpdateCategoria(Categoria categoria);
    Task DeleteCategoria(int id);
}
