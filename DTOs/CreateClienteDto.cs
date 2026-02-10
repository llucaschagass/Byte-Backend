using System.ComponentModel.DataAnnotations;

namespace Byte_Backend.DTOs;

public class CreateClienteDto
{
    /// <summary>
    /// ID da pessoa associada ao cliente
    /// </summary>
    [Required(ErrorMessage = "O ID da pessoa é obrigatório.")]
    public int PessoaId { get; set; }

    /// <summary>
    /// Pontos de fidelidade do cliente
    /// </summary>
    [Range(0, int.MaxValue, ErrorMessage = "Os pontos de fidelidade não podem ser negativos.")]
    public int PontosFidelidade { get; set; } = 0;
}
