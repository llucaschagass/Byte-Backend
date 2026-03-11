namespace Byte_Backend.Entidades;

public class IngredienteReceita : EntidadeBase
{
    public int ReceitaId { get; set; }
    public required string Nome { get; set; }
    public required string Quantidade { get; set; }
    public string? Observacao { get; set; }
    public virtual Receita? Receita { get; set; }
}
