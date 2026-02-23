using Byte_Backend.Entidades;

namespace Byte_Backend.Interfaces;

public interface IComandaRepository
{
    Task<IEnumerable<Comanda>> GetAllComandas();
    Task<Comanda?> GetComandaById(int id);
    Task<IEnumerable<Comanda>> GetComandasByClienteId(int clienteId);
    Task<IEnumerable<Comanda>> GetComandasByCartaoId(int cartaoId);
    Task<Comanda?> GetComandaAbertaByCartaoId(int cartaoId);
    Task CreateComanda(Comanda comanda);
    Task UpdateComanda(Comanda comanda);
    Task DeleteComanda(int id);
}
