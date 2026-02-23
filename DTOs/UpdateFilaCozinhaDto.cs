using System.ComponentModel.DataAnnotations;

namespace Byte_Backend.DTOs;

public class UpdateFilaCozinhaDto
{
    /// <summary>
    /// ID do item da comanda
    /// </summary>
    [Required(ErrorMessage = "O ID do item da comanda é obrigatório.")]
    public int ItemComandaId { get; set; }

    /// <summary>
    /// Status do preparo (Pendente/Preparando/Pronto)
    /// </summary>
    [Required(ErrorMessage = "O status do preparo é obrigatório.")]
    [RegularExpression("^(Pendente|Preparando|Pronto)$", ErrorMessage = "Status deve ser 'Pendente', 'Preparando' ou 'Pronto'.")]
    public required string StatusPreparo { get; set; }
}
