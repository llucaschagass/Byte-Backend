using Byte_Backend.DTOs;
using Byte_Backend.Interfaces;
using BCrypt.Net;

namespace Byte_Backend.Services;

public class AuthService
{
    private readonly IUsuarioRepository usuarioRepository;
    private readonly TokenService tokenService;

    public AuthService(IUsuarioRepository usuarioRepository, TokenService tokenService)
    {
        this.usuarioRepository = usuarioRepository;
        this.tokenService = tokenService;
    }

    public async Task<(bool Success, string Message, LoginResponseDto? Response)> Login(LoginDto loginDto)
    {
        // Buscar usuário pelo login
        var usuario = await usuarioRepository.GetUsuarioByLogin(loginDto.Login);
        
        if (usuario == null)
        {
            return (false, "Login ou senha inválidos.", null);
        }

        // Verificar se o funcionário está ativo
        if (usuario.Funcionario?.Ativo == false)
        {
            return (false, "Usuário inativo. Entre em contato com o administrador.", null);
        }

        // Verificar senha
        try
        {
            if (!BCrypt.Net.BCrypt.Verify(loginDto.Senha, usuario.SenhaHash))
            {
                return (false, "Login ou senha inválidos.", null);
            }
        }
        catch (BCrypt.Net.SaltParseException)
        {
            // Se o hash for inválido, tenta validar como texto plano
            if (loginDto.Senha == usuario.SenhaHash)
            {
                usuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(loginDto.Senha);
                await usuarioRepository.UpdateUsuario(usuario);
            }
            else
            {
                return (false, "Login ou senha inválidos.", null);
            }
        }

        // Gera token JWT
        var token = tokenService.GenerateToken(usuario);
        var expiresAt = tokenService.GetTokenExpiration();

        var response = new LoginResponseDto
        {
            Token = token,
            Login = usuario.Login,
            NomeFuncionario = usuario.Funcionario?.Pessoa?.Nome ?? "Usuário",
            Cargo = usuario.Funcionario?.Cargo?.Nome ?? "Usuario",
            ExpiresAt = expiresAt
        };

        return (true, "Login realizado com sucesso.", response);
    }

    public string HashPassword(string senha)
    {
        return BCrypt.Net.BCrypt.HashPassword(senha);
    }

    public bool VerifyPassword(string senha, string senhaHash)
    {
        return BCrypt.Net.BCrypt.Verify(senha, senhaHash);
    }
}
