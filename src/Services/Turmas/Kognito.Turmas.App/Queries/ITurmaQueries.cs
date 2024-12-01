using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Kognito.Turmas.App.ViewModels;
using Kognito.Turmas.Domain.Interfaces;

namespace Kognito.Turmas.App.Queries;

public interface ITurmaQueries
{   
    /// <summary>
    /// Obtém uma turma pelo seu Id
    /// </summary>
    Task<IEnumerable<TurmaViewModel>> ObterPorId(Guid id);

    /// <summary>
    /// Obtém todas as turmas cadastradas
    /// </summary>
    Task<IEnumerable<TurmaViewModel>> ObterTodasTurmas();
    
    /// <summary>
    /// Obtém todas as turmas de um professor específico
    /// </summary>
    Task<IEnumerable<TurmaViewModel>> ObterTurmasPorProfessor(Guid professorId);

    /// <summary>
    /// Obtém a quantidade de alunos em uma turma
    /// </summary>
    Task<int> ObterQuantidadeAlunos(Guid turmaId);
}
