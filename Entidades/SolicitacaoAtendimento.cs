namespace Byte_Backend.Entidades;

public class SolicitacaoAtendimento : EntidadeBase
{
    public int NumeroMesa { get; set; }
    
    public string Atendida { get; set; } = "N"; // 'S' ou 'N'

    public int? AtendidoPorId { get; set; }
    public virtual Usuario? AtendidoPor { get; set; }

    public DateTime? AtendidoEm { get; set; }
}
