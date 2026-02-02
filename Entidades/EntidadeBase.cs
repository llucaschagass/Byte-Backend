namespace Byte_Backend.Entidades;

public abstract class EntidadeBase
{
    public int Id { get; set; }
    public DateTime InseridoEm { get; set; } = DateTime.Now;
}