using System.ComponentModel.DataAnnotations;

namespace Byte_Backend.Validators;

public class SemEspacosEmBrancoAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success;
        }

        if (value is string texto && string.IsNullOrWhiteSpace(texto))
        {
            return new ValidationResult("O campo não pode ser vazio ou conter apenas espaços em branco.");
        }

        return ValidationResult.Success;
    }
}
