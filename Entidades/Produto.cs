namespace Byte_Backend.Entidades;

public class Produto : EntidadeBase
{
    public int CategoriaId { get; set; }
    public required string Nome { get; set; }
    public decimal Preco { get; set; }
    public string? Descricao { get; set; }
    public bool Ativo { get; set; } = true;
    public virtual Categoria? Categoria { get; set; }
    public virtual ICollection<OpcaoProduto> Opcoes { get; set; } = new List<OpcaoProduto>();
}
