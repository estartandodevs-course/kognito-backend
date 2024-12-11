
using System.ComponentModel.DataAnnotations;

namespace Kognito.WebApi.InputModels;

/// <summary>
/// Modelo para criação de um novo professor
/// </summary>
public class ProfessorInputModel
{
    /// <summary>
    /// Nome completo do professor
    /// </summary>
    /// <example>Maria Santos</example>
    [Required(ErrorMessage = "Informe um nome!")]
    public string Name { get; set; }

    /// <summary>
    /// CPF do professor
    /// </summary>
    /// <example>12345678900</example>
    [Required(ErrorMessage = "Informe um CPF!")]
    public string Cpf { get; set; }

    /// <summary>
    /// Email do professor
    /// </summary>
    /// <example>maria.santos@email.com</example>
    [Required(AllowEmptyStrings = true)]
    [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido...")]
    public string Email { get; set; }

    /// <summary>
    /// Senha de acesso do professor
    /// </summary>
    /// <example>Senha@123</example>
    [Required(ErrorMessage = "Informe uma senha!")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 5)]
    public string Password { get; set; }
}
