namespace Byte_Backend.Entidades;

public class Receita : EntidadeBase
{
    public int ProdutoId { get; set; }
    public required string Descricao { get; set; }
    public int TempoPreparo { get; set; }
    public virtual Produto? Produto { get; set; }
    public virtual ICollection<IngredienteReceita> Ingredientes { get; set; } = new List<IngredienteReceita>();
}
