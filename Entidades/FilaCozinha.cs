namespace Byte_Backend.Entidades;

public class FilaCozinha : EntidadeBase
{
    public int ItemComandaId { get; set; }
    public virtual ItemComanda? ItemComanda { get; set; }
    
    public required string StatusPreparo { get; set; } // Pendente/Preparando/Pronto
    public DateTime UltimaAtualizacao { get; set; } = DateTime.Now;
}
