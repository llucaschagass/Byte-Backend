namespace Byte_Backend.Entidades;

public class Usuario : EntidadeBase
{
    public int FuncionarioId { get; set; }
    public virtual Funcionario? Funcionario { get; set; }

    public required string Login { get; set; }
    public required string SenhaHash { get; set; }
}
