using System.ComponentModel.DataAnnotations;

namespace Byte_Backend.DTOs;

public class AtenderSolicitacaoDto
{
    /// <summary>
    /// ID do usuário (garçom) que atendeu a solicitação
    /// </summary>
    [Required(ErrorMessage = "O ID do usuário que atendeu é obrigatório.")]
    public int UsuarioId { get; set; }
}
