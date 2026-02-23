using System.ComponentModel.DataAnnotations;

namespace Byte_Backend.DTOs;

public class CreateOpcaoProdutoDto
{
    [Required(ErrorMessage = "O produto é obrigatório")]
    public int ProdutoId { get; set; }

    [Required(ErrorMessage = "O nome da opção é obrigatório")]
    [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
    public required string Nome { get; set; }

    [Required(ErrorMessage = "O preço adicional é obrigatório")]
    [Range(0, double.MaxValue, ErrorMessage = "O preço adicional não pode ser negativo")]
    public decimal PrecoAdicional { get; set; }
}
