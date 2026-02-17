using Byte_Backend.Entidades;

namespace Byte_Backend.Interfaces;

public interface IFilaCozinhaRepository
{
    Task<IEnumerable<FilaCozinha>> GetAllFilasCozinha();
    Task<FilaCozinha?> GetFilaCozinhaById(int id);
    Task<IEnumerable<FilaCozinha>> GetFilasCozinhaByStatus(string status);
    Task<IEnumerable<FilaCozinha>> GetFilasCozinhaByItemComandaId(int itemComandaId);
    Task CreateFilaCozinha(FilaCozinha filaCozinha);
    Task UpdateFilaCozinha(FilaCozinha filaCozinha);
    Task DeleteFilaCozinha(int id);
}
