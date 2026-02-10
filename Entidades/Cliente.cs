namespace Byte_Backend.Entidades;

public class Cliente : EntidadeBase
{
    public int PessoaId { get; set; }
    public virtual Pessoa? Pessoa { get; set; }

    public int PontosFidelidade { get; set; } = 0;
}
