using System.ComponentModel.DataAnnotations;

namespace Byte_Backend.DTOs;

public class CreateReceitaDto
{
    [Required(ErrorMessage = "O produto é obrigatório")]
    public int ProdutoId { get; set; }

    [Required(ErrorMessage = "A descrição é obrigatória")]
    [StringLength(200, ErrorMessage = "A descrição deve ter no máximo 200 caracteres")]
    public required string Descricao { get; set; }

    [Required(ErrorMessage = "O tempo de preparo é obrigatório")]
    [Range(1, int.MaxValue, ErrorMessage = "O tempo de preparo deve ser no mínimo 1 minuto")]
    public int TempoPreparo { get; set; }
}
