namespace Byte_Backend.DTOs;

public class LoginResponseDto
{
    public required string Token { get; set; }
    public required string Login { get; set; }
    public int UsuarioId { get; set; }
    public required string NomeFuncionario { get; set; }
    public required string Cargo { get; set; }
    public DateTime ExpiresAt { get; set; }
}
