using System.ComponentModel.DataAnnotations;

namespace Kognito.WebApi.InputModels;

public class AdicionarNeurodivergenciaInputModel
{
    [Required(ErrorMessage = "O código do responsável é obrigatório")]
    public Guid CodigoPai { get; set; }

    [Required(ErrorMessage = "A neurodivergência é obrigatória")]
    public string Neurodivergencia { get; set; }
}