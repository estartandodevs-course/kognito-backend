using System;
using Kognito.Turmas.App.ViewModels;
namespace Kognito.Turmas.App.Queries;

public interface ITurmaQueries
{   
    /// <summary>
    /// Obtém uma turma pelo seu Id
    /// </summary>
    Task<TurmaViewModel> ObterPorId(Guid id);

    /// <summary>
    /// Obtém todas as turmas cadastradas
    /// </summary>
    Task<IEnumerable<TurmaViewModel>> ObterTodasTurmas();
    
    /// <summary>
    /// Obtém todas as turmas de um professor específico
    /// </summary>
    Task<IEnumerable<TurmaViewModel>> ObterTurmasPorProfessor(Guid professorId);
}
