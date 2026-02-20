using System.ComponentModel.DataAnnotations;

namespace Byte_Backend.Entidades;

public class ProdutoImagem
{
    [Key]
    public int ProdutoImagemId { get; set; }

    public int ProdutoId { get; set; }

    public required string Conteudo { get; set; }

    public DateTime InseridoEm { get; set; } = DateTime.UtcNow;

    public virtual Produto? Produto { get; set; }
}
