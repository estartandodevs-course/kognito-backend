using System.ComponentModel.DataAnnotations;

namespace Kognito.Turmas.Domain;

public enum Cor
{
    [Display(Name = "Vermelho")]
    Vermelho = 1,
    [Display(Name = "Azul")]
    Azul = 2,
    [Display(Name = "Verde")]
    Verde = 3,
    [Display(Name = "Amarelo")]
    Amarelo = 4
}