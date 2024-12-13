namespace Kognito.WebApi.InputModels;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Modelo para criação de um novo aluno
/// </summary>
public class AlunoInputModel
{
    /// <summary>
    /// Nome completo do aluno
    /// </summary>
    /// <example>João da Silva</example>
    [Required(ErrorMessage = "Informe um nome!")]
    public string Name { get; set; }

    /// <summary>
    /// CPF do aluno
    /// </summary>
    /// <example>12345678900</example>
    [Required(ErrorMessage = "Informe um CPF!")]
    public string Cpf { get; set; }

    /// <summary>
    /// Email do aluno
    /// </summary>
    /// <example>joao.silva@email.com</example>
    [Required(AllowEmptyStrings = true)]
    [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido...")]
    public string Email { get; set; }

    /// <summary>
    /// Senha de acesso do aluno
    /// </summary>
    /// <example>Senha@123</example>
    [Required(ErrorMessage = "Informe uma senha!")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 5)]
    public string Password { get; set; }

    /// <summary>
    /// Email do responsável pelo aluno
    /// </summary>
    /// <example>responsavel@email.com</example>
    [Required(ErrorMessage = "Informe o email do responsável!")]
    [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido para o responsável...")]
    public string parentEmail { get; set; }
}