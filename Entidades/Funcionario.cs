namespace Byte_Backend.Entidades;

public class Funcionario : EntidadeBase
{
    public int PessoaId { get; set; }
    public virtual Pessoa? Pessoa { get; set; }

    public int CargoId { get; set; }
    public virtual Cargo? Cargo { get; set; }

    public string? CodigoFuncionario { get; set; }
    public bool Ativo { get; set; } = true;
}