namespace Kognito.WebApi.InputModels;
using System.ComponentModel.DataAnnotations;


/// <summary>
/// Modelo para autenticação de usuário
/// </summary>
public class UsuarioLogin
{
    /// <summary>
    /// Email do usuário
    /// </summary>
    /// <example>usuario@email.com</example>
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string Email { get; set; }

    /// <summary>
    /// Senha do usuário
    /// </summary>
    /// <example>Senha@123</example>
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 5)]
    public string Password { get; set; }
}