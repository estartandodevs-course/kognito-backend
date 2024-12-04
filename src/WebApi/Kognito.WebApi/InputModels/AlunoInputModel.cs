namespace Kognito.WebApi.InputModels;

using System.ComponentModel.DataAnnotations;

public class AlunoInputModel
{
    [Required(ErrorMessage = "Informe um nome!")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Informe um CPF!")]
    public string Cpf { get; set; }

    [Required(AllowEmptyStrings = true)]
    [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido...")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Informe uma senha!")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 5)]
    public string Password { get; set; }

    [Required(ErrorMessage = "Informe o email do responsável!")]
    [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido para o responsável...")]
    public string EmailResponsavel { get; set; }
}