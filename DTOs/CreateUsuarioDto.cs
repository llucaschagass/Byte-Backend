using System.ComponentModel.DataAnnotations;

namespace Byte_Backend.DTOs;

public class CreateUsuarioDto
{
    [Required(ErrorMessage = "O FuncionarioId é obrigatório")]
    [Range(1, int.MaxValue, ErrorMessage = "O FuncionarioId deve ser maior que zero")]
    public int FuncionarioId { get; set; }

    [Required(ErrorMessage = "O login é obrigatório")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "O login deve ter entre 3 e 50 caracteres")]
    public required string Login { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 100 caracteres")]
    public required string Senha { get; set; }
}
