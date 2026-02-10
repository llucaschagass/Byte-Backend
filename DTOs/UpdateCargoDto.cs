using System.ComponentModel.DataAnnotations;
using Byte_Backend.Validators;

namespace Byte_Backend.DTOs;

public class UpdateCargoDto
{
    [Required(ErrorMessage = "O nome do cargo é obrigatório")]
    [SemEspacosEmBranco]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 100 caracteres")]
    public required string Nome { get; set; }
    
    [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres")]
    public string? Descricao { get; set; }
}
