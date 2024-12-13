using System.ComponentModel.DataAnnotations;

/// <summary>
/// Modelo para criação ou atualização de uma meta
/// </summary>
public class MetaInputModel
{
    /// <summary>
    /// Título da meta
    /// </summary>
    /// <example>Aprender C#</example>
    [Required(ErrorMessage = "O título é obrigatório")]
    [StringLength(100, ErrorMessage = "O título deve ter no máximo 100 caracteres")]
    [MinLength(1, ErrorMessage = "O título deve ter no mínimo 1 caractere")]
    public string Title { get; set; }

    /// <summary>
    /// Descrição detalhada da meta
    /// </summary>
    /// <example>Dominar a linguagem C# incluindo LINQ e programação assíncrona</example>
    [Required(ErrorMessage = "A descrição é obrigatória")]
    [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres")]
    [MinLength(1, ErrorMessage = "A descrição deve ter no mínimo 1 caractere")]
    public string Description { get; set; }
}