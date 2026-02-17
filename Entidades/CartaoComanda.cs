namespace Byte_Backend.Entidades;

public class CartaoComanda : EntidadeBase
{
    public int NumeroCartao { get; set; }
    public required string CodigoRfid { get; set; }
    
    public virtual ICollection<Comanda> Comandas { get; set; } = new List<Comanda>();
}
