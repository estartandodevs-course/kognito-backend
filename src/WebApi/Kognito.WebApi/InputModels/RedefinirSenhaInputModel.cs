using System.ComponentModel.DataAnnotations;

namespace Kognito.WebApi.InputModels;

public class RedefinirSenhaInputModel
{
    [Required(ErrorMessage = "O email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O código de recuperação é obrigatório")]
    public string RecoveryCode { get; set; }

    [Required(ErrorMessage = "A nova senha é obrigatória")]
    [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres")]
    public string NewPassword { get; set; }

    [Compare("NovaSenha", ErrorMessage = "As senhas não conferem")]
    public string ConfirmPassword { get; set; }
}