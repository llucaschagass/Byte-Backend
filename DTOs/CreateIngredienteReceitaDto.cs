using System.ComponentModel.DataAnnotations;

namespace Byte_Backend.DTOs;

public class CreateIngredienteReceitaDto
{
    [Required(ErrorMessage = "A receita é obrigatória")]
    public int ReceitaId { get; set; }

    [Required(ErrorMessage = "O nome do ingrediente é obrigatório")]
    [StringLength(150, ErrorMessage = "O nome deve ter no máximo 150 caracteres")]
    public required string Nome { get; set; }

    [Required(ErrorMessage = "A quantidade é obrigatória")]
    [StringLength(50, ErrorMessage = "A quantidade deve ter no máximo 50 caracteres")]
    public required string Quantidade { get; set; }

    [StringLength(200, ErrorMessage = "A observação deve ter no máximo 200 caracteres")]
    public string? Observacao { get; set; }
}
