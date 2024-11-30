using System.ComponentModel.DataAnnotations;

namespace Kognito.Turmas.Domain;

public enum Icones
{
    [Display(Name = "Primeiro Ícone")]
    Primeiro = 1,
    [Display(Name = "Segundo Ícone")]
    Segundo = 2,
    [Display(Name = "Terceiro Ícone")]
    Terceiro = 3
}