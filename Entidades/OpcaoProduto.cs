namespace Byte_Backend.Entidades;

public class OpcaoProduto : EntidadeBase
{
    public int ProdutoId { get; set; }
    public required string Nome { get; set; }
    public decimal PrecoAdicional { get; set; } // Valor que será somado ao preço base do produto
    
    // Relacionamento
    public virtual Produto? Produto { get; set; }
}
