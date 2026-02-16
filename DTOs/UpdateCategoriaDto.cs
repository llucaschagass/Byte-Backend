using System.ComponentModel.DataAnnotations;

namespace Byte_Backend.DTOs;

public class UpdateCategoriaDto
{
    [Required(ErrorMessage = "O nome da categoria é obrigatório")]
    [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
    public required string Nome { get; set; }

    [StringLength(255, ErrorMessage = "A descrição deve ter no máximo 255 caracteres")]
    public string? Descricao { get; set; }
}
