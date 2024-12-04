using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using Kognito.Turmas.Domain;

namespace Kognito.WebApi.InputModels;

/// <summary>
/// Modelo para criação ou atualização de uma turma
/// </summary>
public class TurmaInputModel
{
    /// <summary>
    /// Nome da turma
    /// </summary>
    /// <example>Programação Básica</example>
    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
    public string Name { get; set; }

    /// <summary>
    /// Descrição da turma
    /// </summary>
    /// <example>Introdução aos conceitos e práticas de programação</example>
    [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres")]
    public string Description { get; set; }

    /// <summary>
    /// Matéria da turma
    /// </summary>
    /// <example>Ciência da Computação</example>
    [Required(ErrorMessage = "A matéria é obrigatória")]
    public string Subject { get; set; }

    /// <summary>
    /// Cor tema da turma
    /// </summary>
    [Required(ErrorMessage = "A cor é obrigatória")]
    public Cor Color { get; set; }

    /// <summary>
    /// Identificador do ícone da turma
    /// </summary>
    [Required(ErrorMessage = "O ícone é obrigatório")]
    public Icones Icon { get; set; }
}