namespace Kognito.WebApi.InputModels;
using System.ComponentModel.DataAnnotations;


public class UsuarioLogin
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 5)]
    public string Password { get; set; }
}