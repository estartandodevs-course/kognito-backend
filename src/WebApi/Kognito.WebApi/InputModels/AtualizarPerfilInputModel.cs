namespace Kognito.WebApi.InputModels;

using System.ComponentModel.DataAnnotations;

public class AtualizarPerfilInputModel
{
    [Required(ErrorMessage = "Informe um nome!")]
    public string Name { get; set; }

    [Required(AllowEmptyStrings = true)]
    [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido...")]
    public string Email { get; set; }
}