using System.ComponentModel.DataAnnotations;

namespace Byte_Backend.DTOs;

public class CreateSolicitacaoAtendimentoDto
{
    /// <summary>
    /// Número da mesa que está chamando o garçom
    /// </summary>
    [Required(ErrorMessage = "O número da mesa é obrigatório.")]
    [Range(1, int.MaxValue, ErrorMessage = "O número da mesa deve ser maior que zero.")]
    public int NumeroMesa { get; set; }
}
