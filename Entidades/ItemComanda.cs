namespace Byte_Backend.Entidades;

public class ItemComanda : EntidadeBase
{
    public int ComandaId { get; set; }
    public virtual Comanda? Comanda { get; set; }
    
    public int ProdutoId { get; set; }
    public virtual Produto? Produto { get; set; }
    
    public int? OpcaoProdutoId { get; set; }
    public virtual OpcaoProduto? OpcaoProduto { get; set; }
    
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
    public string? Observacao { get; set; }
    
    public virtual FilaCozinha? FilaCozinha { get; set; }
}
