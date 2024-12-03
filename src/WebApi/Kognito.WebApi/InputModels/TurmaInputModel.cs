using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using Kognito.Turmas.Domain;

namespace Kognito.WebApi.InputModels;

public class TurmaInputModel
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
    public string Name { get; set; }

    [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres")]
    public string Description { get; set; }

    [Required(ErrorMessage = "A matéria é obrigatória")]
    public string Subject { get; set; }

    [Required(ErrorMessage = "A cor é obrigatória")]
    public Cor Color { get; set; }

    [Required(ErrorMessage = "O ícone é obrigatório")]
    public Icones Icon { get; set; }
}