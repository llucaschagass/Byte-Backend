using System.ComponentModel.DataAnnotations;

namespace Byte_Backend.DTOs;

public class CreateProdutoImagemDto
{
    [Required(ErrorMessage = "O ID do produto é obrigatório")]
    public int ProdutoId { get; set; }

    [Required(ErrorMessage = "O conteúdo da imagem é obrigatório")]
    public required string Conteudo { get; set; }
}
