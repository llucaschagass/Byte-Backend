using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Byte_Backend.Validators;

/// <summary>
/// Validador de CPF brasileiro
/// </summary>
public class CpfValidoAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
        {
            return ValidationResult.Success; // CPF é opcional
        }

        var cpf = value.ToString()!.Replace(".", "").Replace("-", "").Trim();

        if (cpf.Length != 11)
        {
            return new ValidationResult("CPF deve conter 11 dígitos");
        }

        if (!cpf.All(char.IsDigit))
        {
            return new ValidationResult("CPF deve conter apenas números");
        }

        // Verificar se todos os dígitos são iguais (CPF inválido)
        if (cpf.Distinct().Count() == 1)
        {
            return new ValidationResult("CPF inválido");
        }

        // Validar dígitos verificadores
        var multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        var multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        var tempCpf = cpf.Substring(0, 9);
        var soma = 0;

        for (int i = 0; i < 9; i++)
        {
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
        }

        var resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        var digito = resto.ToString();
        tempCpf = tempCpf + digito;
        soma = 0;

        for (int i = 0; i < 10; i++)
        {
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
        }

        resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;
        digito = digito + resto.ToString();

        if (!cpf.EndsWith(digito))
        {
            return new ValidationResult("CPF inválido");
        }

        return ValidationResult.Success;
    }
}
