using System.ComponentModel.DataAnnotations;

namespace Byte_Backend.DTOs;

public class CreateFuncionarioDto
{
    [Required(ErrorMessage = "O ID da pessoa é obrigatório")]
    [Range(1, int.MaxValue, ErrorMessage = "O ID da pessoa deve ser maior que zero")]
    public int PessoaId { get; set; }
    
    [Required(ErrorMessage = "O ID do cargo é obrigatório")]
    [Range(1, int.MaxValue, ErrorMessage = "O ID do cargo deve ser maior que zero")]
    public int CargoId { get; set; }
}
