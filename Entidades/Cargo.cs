namespace Byte_Backend.Entidades;

public class Cargo : EntidadeBase
{
    public required string Nome { get; set; }
    public string? Descricao { get; set; }
}