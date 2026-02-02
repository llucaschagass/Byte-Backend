namespace Byte_Backend.Entidades;

public class Pessoa : EntidadeBase
{
    public required string Nome { get; set; }
    public string? Email { get; set; }
    public string? CPF { get; set; }
    public string? Telefone { get; set; }
}