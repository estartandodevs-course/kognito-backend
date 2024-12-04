using System.ComponentModel.DataAnnotations;
namespace Kognito.WebApi.InputModels;

/// <summary>
/// Modelo para alteração de senha do usuário
/// </summary>
public class AlterarSenhaInputModel
{
    /// <summary>
    /// Senha atual do usuário
    /// </summary>
    /// <example>SenhaAtual123</example>
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 5)]
    public string CurrentPassword { get; set; }

    /// <summary>
    /// Nova senha a ser definida
    /// </summary>
    /// <example>NovaSenhaSegura456</example>
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 5)]
    public string NewPassword { get; set; }
}