using System.ComponentModel.DataAnnotations;

namespace Byte_Backend.DTOs;

public class UpdateCargoDto
{
    [Required(ErrorMessage = "O nome do cargo é obrigatório")]
    [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
    public required string Nome { get; set; }
    
    [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres")]
    public string? Descricao { get; set; }
}
