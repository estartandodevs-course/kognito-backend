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
    public Guid AlunoId { get; set; }

    /// <summary>
    /// Nome completo do aluno
    /// </summary>
    /// <example>João da Silva</example>
    public string AlunoNome { get; set; }
} 