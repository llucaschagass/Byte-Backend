using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Byte_Backend.Entidades;
using Microsoft.IdentityModel.Tokens;

namespace Byte_Backend.Services;

public class TokenService
{
    private readonly IConfiguration configuration;

    public TokenService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public string GenerateToken(Usuario usuario)
    {
        var secretKey = configuration["JwtSettings:SecretKey"] 
            ?? throw new InvalidOperationException("JWT SecretKey não configurada.");
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.Login),
            new Claim(ClaimTypes.Role, usuario.Funcionario?.Cargo?.Nome ?? "Usuario"),
            new Claim("FuncionarioId", usuario.FuncionarioId.ToString()),
            new Claim("CargoId", usuario.Funcionario?.CargoId.ToString() ?? "0")
        };

        var expirationHours = int.Parse(configuration["JwtSettings:ExpirationHours"] ?? "8");
        
        var token = new JwtSecurityToken(
            issuer: configuration["JwtSettings:Issuer"],
            audience: configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(expirationHours),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public DateTime GetTokenExpiration()
    {
        var expirationHours = int.Parse(configuration["JwtSettings:ExpirationHours"] ?? "8");
        return DateTime.UtcNow.AddHours(expirationHours);
    }
}
