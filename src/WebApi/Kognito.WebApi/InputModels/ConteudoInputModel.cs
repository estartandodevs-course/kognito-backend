using System.ComponentModel.DataAnnotations;

namespace Kognito.WebApi.InputModels;

/// <summary>
/// Modelo para criação ou atualização de conteúdo educacional
/// </summary>
public class ConteudoInputModel
{
    /// <summary>
    /// Título do conteúdo
    /// </summary>
    /// <example>Introdução à Programação</example>
    [Required(ErrorMessage = "O título é obrigatório")]
    public string Title { get; set; }

    /// <summary>
    /// Descrição do conteúdo
    /// </summary>
    /// <example>Conceitos básicos de lógica de programação e algoritmos</example>
    [Required(ErrorMessage = "A descrição é obrigatória")]
    public string Description { get; set; }

    /// <summary>
    /// ID da turma à qual este conteúdo pertence
    /// </summary>
    /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
    [Required(ErrorMessage = "A turma é obrigatória")]
    public Guid ClassId { get; set; }
}