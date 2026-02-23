using System.ComponentModel.DataAnnotations;

namespace Byte_Backend.DTOs;

public class UpdateCartaoComandaDto
{
    /// <summary>
    /// Número do cartão comanda
    /// </summary>
    [Required(ErrorMessage = "O número do cartão é obrigatório.")]
    [Range(1, int.MaxValue, ErrorMessage = "O número do cartão deve ser maior que zero.")]
    public int NumeroCartao { get; set; }

    /// <summary>
    /// Código RFID do cartão
    /// </summary>
    [Required(ErrorMessage = "O código RFID é obrigatório.")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "O código RFID deve ter entre 1 e 100 caracteres.")]
    public required string CodigoRfid { get; set; }
}
