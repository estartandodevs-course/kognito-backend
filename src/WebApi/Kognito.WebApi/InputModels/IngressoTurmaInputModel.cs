using System.ComponentModel.DataAnnotations;

namespace Kognito.WebApi.InputModels;

/// <summary>
/// Modelo para ingresso de um aluno em uma turma
/// </summary>
public class IngressoTurmaInputModel
{
    /// <summary>
    /// Identificador único do aluno
    /// </summary>
    /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
    [Required(ErrorMessage = "O ID do aluno é obrigatório")]

   public Guid StudentId { get; set; } 

    /// <summary>
    /// Nome completo do aluno
    /// </summary>
    /// <example>João da Silva</example>
    [Required(ErrorMessage = "O nome do aluno é obrigatório")]
    public string StudentName { get; set; }
} 