using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Byte_Backend.Validators;

/// <summary>
/// Validador de telefone brasileiro
/// Aceita formatos: (11) 98765-4321, (11) 3456-7890, 11987654321, etc.
/// </summary>
public class TelefoneValidoAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
        {
            return ValidationResult.Success; // Telefone é opcional
        }

        var telefone = value.ToString()!.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Trim();

        if (!telefone.All(char.IsDigit))
        {
            return new ValidationResult("Telefone deve conter apenas números");
        }

        // Telefone fixo: 10 dígitos (DDD + 8 dígitos)
        // Telefone celular: 11 dígitos (DDD + 9 dígitos)
        if (telefone.Length != 10 && telefone.Length != 11)
        {
            return new ValidationResult("Telefone deve ter 10 ou 11 dígitos (com DDD)");
        }

        // Validar DDD (11 a 99)
        var ddd = int.Parse(telefone.Substring(0, 2));
        if (ddd < 11 || ddd > 99)
        {
            return new ValidationResult("DDD inválido");
        }

        return ValidationResult.Success;
    }
}
