using Byte_Backend.Entidades;

namespace Byte_Backend.Interfaces;

public interface ICartaoComandaRepository
{
    Task<IEnumerable<CartaoComanda>> GetAllCartoesComanda();
    Task<CartaoComanda?> GetCartaoComandaById(int id);
    Task<CartaoComanda?> GetCartaoComandaByNumeroCartao(int numeroCartao);
    Task<CartaoComanda?> GetCartaoComandaByCodigoRfid(string codigoRfid);
    Task CreateCartaoComanda(CartaoComanda cartao);
    Task UpdateCartaoComanda(CartaoComanda cartao);
    Task DeleteCartaoComanda(int id);
}
