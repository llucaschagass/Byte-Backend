namespace Byte_Backend.Entidades;

public class Comanda : EntidadeBase
{
    public int CartaoId { get; set; }
    public virtual CartaoComanda? Cartao { get; set; }
    
    public int ClienteId { get; set; }
    public virtual Cliente? Cliente { get; set; }
    
    public required string Status { get; set; } // Aberta/Fechada
    public DateTime AbertaEm { get; set; } = DateTime.Now;
    public DateTime? FechadaEm { get; set; }
    
    public virtual ICollection<ItemComanda> Itens { get; set; } = new List<ItemComanda>();
}
