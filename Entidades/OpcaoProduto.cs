namespace Byte_Backend.Entidades;

public class OpcaoProduto : EntidadeBase
{
    public int ProdutoId { get; set; }
    public required string Nome { get; set; }
    public decimal PrecoAdicional { get; set; }
    public virtual Produto? Produto { get; set; }
}
