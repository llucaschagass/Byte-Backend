using System.ComponentModel.DataAnnotations;

namespace Byte_Backend.DTOs;

public class CreatePessoaDto
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(200, ErrorMessage = "O nome deve ter no máximo 200 caracteres")]
    public required string Nome { get; set; }
    
    [EmailAddress(ErrorMessage = "Email inválido")]
    [StringLength(100, ErrorMessage = "O email deve ter no máximo 100 caracteres")]
    public string? Email { get; set; }
    
    [StringLength(14, ErrorMessage = "O CPF deve ter no máximo 14 caracteres")]
    public string? CPF { get; set; }
    
    [Phone(ErrorMessage = "Telefone inválido")]
    [StringLength(20, ErrorMessage = "O telefone deve ter no máximo 20 caracteres")]
    public string? Telefone { get; set; }
}
