using System.ComponentModel.DataAnnotations;

namespace Byte_Backend.DTOs;

public class CreateItemComandaDto
{
    /// <summary>
    /// ID da comanda
    /// </summary>
    [Required(ErrorMessage = "O ID da comanda é obrigatório.")]
    public int ComandaId { get; set; }

    /// <summary>
    /// ID do produto
    /// </summary>
    [Required(ErrorMessage = "O ID do produto é obrigatório.")]
    public int ProdutoId { get; set; }

    /// <summary>
    /// ID da opção do produto (opcional)
    /// </summary>
    public int? OpcaoProdutoId { get; set; }

    /// <summary>
    /// Quantidade do produto
    /// </summary>
    [Required(ErrorMessage = "A quantidade é obrigatória.")]
    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
    public int Quantidade { get; set; }

    /// <summary>
    /// Preço unitário do produto
    /// </summary>
    [Required(ErrorMessage = "O preço unitário é obrigatório.")]
    [Range(0, double.MaxValue, ErrorMessage = "O preço unitário não pode ser negativo.")]
    public decimal PrecoUnitario { get; set; }

    /// <summary>
    /// Observação sobre o item (opcional)
    /// </summary>
    [StringLength(500, ErrorMessage = "A observação deve ter no máximo 500 caracteres.")]
    public string? Observacao { get; set; }
}
