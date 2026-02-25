using System.ComponentModel.DataAnnotations;
using Byte_Backend.Entidades;

namespace Byte_Backend.DTOs;

public class CreateUsuarioPermissaoDto
{
    [Required(ErrorMessage = "O ID do usuário é obrigatório.")]
    public int UsuarioId { get; set; }

    public bool Garcom { get; set; }
    public bool Cozinha { get; set; }
    public bool Gerencia { get; set; }

    [Required(ErrorMessage = "A permissão principal é obrigatória.")]
    [EnumDataType(typeof(PermissaoPrincipal), ErrorMessage = "Valor inválido para Principal. Use G (Garçom), C (Cozinha) ou P (Gerência).")]
    public PermissaoPrincipal Principal { get; set; }
}
