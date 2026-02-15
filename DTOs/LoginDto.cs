using System.ComponentModel.DataAnnotations;

namespace Byte_Backend.DTOs;

public class LoginDto
{
    /// <summary>
    /// Login do usuário
    /// </summary>
    [Required(ErrorMessage = "O login é obrigatório.")]
    public required string Login { get; set; }

    /// <summary>
    /// Senha do usuário
    /// </summary>
    [Required(ErrorMessage = "A senha é obrigatória.")]
    public required string Senha { get; set; }
}
