using Byte_Backend.Entidades;

namespace Byte_Backend.Interfaces;

public interface IItemComandaRepository
{
    Task<IEnumerable<ItemComanda>> GetAllItensComanda();
    Task<ItemComanda?> GetItemComandaById(int id);
    Task<IEnumerable<ItemComanda>> GetItensByComandaId(int comandaId);
    Task CreateItemComanda(ItemComanda item);
    Task UpdateItemComanda(ItemComanda item);
    Task DeleteItemComanda(int id);
}
