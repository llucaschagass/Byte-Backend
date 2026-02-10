using System.ComponentModel.DataAnnotations;

namespace Byte_Backend.DTOs;

public class CreateFuncionarioDto
{
    [Required(ErrorMessage = "O ID da pessoa é obrigatório")]
    public int PessoaId { get; set; }
    
    [Required(ErrorMessage = "O ID do cargo é obrigatório")]
    public int CargoId { get; set; }
}
