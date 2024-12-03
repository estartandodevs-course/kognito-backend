using System.ComponentModel.DataAnnotations;

namespace Kognito.WebApi.InputModels;

public class ConteudoInputModel
{
    [Required(ErrorMessage = "O título é obrigatório")]
    public string Title { get; set; }

    [Required(ErrorMessage = "A descrição é obrigatória")]
    public string Description { get; set; }

    [Required(ErrorMessage = "A turma é obrigatória")]
    public Guid ClassId { get; set; }
}