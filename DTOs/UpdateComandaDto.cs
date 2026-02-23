using System.ComponentModel.DataAnnotations;

namespace Byte_Backend.DTOs;

public class UpdateComandaDto
{
    /// <summary>
    /// ID do cartão comanda
    /// </summary>
    [Required(ErrorMessage = "O ID do cartão é obrigatório.")]
    public int CartaoId { get; set; }

    /// <summary>
    /// ID do cliente
    /// </summary>
    [Required(ErrorMessage = "O ID do cliente é obrigatório.")]
    public int ClienteId { get; set; }

    /// <summary>
    /// Status da comanda (Aberta/Fechada)
    /// </summary>
    [Required(ErrorMessage = "O status é obrigatório.")]
    [RegularExpression("^(Aberta|Fechada)$", ErrorMessage = "Status deve ser 'Aberta' ou 'Fechada'.")]
    public required string Status { get; set; }

    /// <summary>
    /// Data/hora de abertura da comanda
    /// </summary>
    [Required(ErrorMessage = "A data de abertura é obrigatória.")]
    public DateTime AbertaEm { get; set; }

    /// <summary>
    /// Data/hora de fechamento da comanda (opcional)
    /// </summary>
    public DateTime? FechadaEm { get; set; }
}
