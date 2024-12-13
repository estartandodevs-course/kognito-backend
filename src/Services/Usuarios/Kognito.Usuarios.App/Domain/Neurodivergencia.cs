using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Kognito.Usuarios.App.Domain;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Neurodivergencia
{
    [Display(Name = "TDAH")]
    TDAH = 1,
    [Display(Name = "Autismo")]
    Autismo = 2,
    [Display(Name = "Dislexia")]
    Dislexia = 3,
    [Display(Name = "Discalculia")]
    Discalculia = 4,
    [Display(Name = "Disgrafia")]
    Disgrafia = 5,
    [Display(Name = "TOC")]
    TOC = 6,
    [Display(Name = "Tourette")]
    Tourette = 7,
    [Display(Name = "Ansiedade")]
    Ansiedade = 8,
    [Display(Name = "Bipolaridade")]
    Bipolaridade = 9,
    [Display(Name = "Depressão")]
    Depressao = 10
}