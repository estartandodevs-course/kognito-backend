using System.ComponentModel.DataAnnotations;

namespace Kognito.WebApi.InputModels;

public class EsqueceuSenhaInputModel
{
    [Required(ErrorMessage = "O email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; }
}