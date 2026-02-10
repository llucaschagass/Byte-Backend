using System.ComponentModel.DataAnnotations;
using Byte_Backend.Validators;

namespace Byte_Backend.DTOs;

public class CreatePessoaDto
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    [SemEspacosEmBranco]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 200 caracteres")]
    public required string Nome { get; set; }
    
    [EmailAddress(ErrorMessage = "Email inválido")]
    [StringLength(100, ErrorMessage = "O email deve ter no máximo 100 caracteres")]
    public string? Email { get; set; }
    
    [CpfValido]
    [StringLength(14, ErrorMessage = "O CPF deve ter no máximo 14 caracteres")]
    public string? CPF { get; set; }
    
    [TelefoneValido]
    [StringLength(20, ErrorMessage = "O telefone deve ter no máximo 20 caracteres")]
    public string? Telefone { get; set; }
}
