using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Kognito.Turmas.Domain;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Icones
{
    [Display(Name = "assets/img/icons/Biologia.svg")]
    Biologia,
    
    [Display(Name = "assets/img/icons/Fisica.svg")]
    Fisica,
    
    [Display(Name = "assets/img/icons/Historia.svg")]
    Historia,
    
    [Display(Name = "assets/img/icons/Matematica.svg")]
    Matematica,
    
    [Display(Name = "assets/img/icons/Portugues.svg")]
    Portugues,
    
    [Display(Name = "assets/img/icons/Quimica.svg")]
    Quimica
}