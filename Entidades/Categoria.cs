using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Byte_Backend.Entidades;

public class Categoria
{
    [Key]
    public int CategoriaId { get; set; }
    
    public required string Nome { get; set; }
    public string? Descricao { get; set; }
    
    public DateTime InseridoEm { get; set; } = DateTime.Now;
}
