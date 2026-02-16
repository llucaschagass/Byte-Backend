namespace Byte_Backend.Entidades;

public class Categoria : EntidadeBase
{
    public required string Nome { get; set; }
    public string? Descricao { get; set; }
}
