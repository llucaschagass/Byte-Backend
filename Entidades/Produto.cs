namespace Byte_Backend.Entidades;

public class Produto : EntidadeBase
{
    public int CategoriaId { get; set; }
    public required string Nome { get; set; }
    public decimal Preco { get; set; } // O preço base do item
    public string? Descricao { get; set; }
    public bool Ativo { get; set; } = true;
    
    // Relacionamento
    public virtual Categoria? Categoria { get; set; }
}
