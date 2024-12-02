using Kognito.Turmas.App.ViewModels;

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

    /// <summary>
    /// Valida se o hash de acesso fornecido corresponde ao hash da turma
    /// </summary>
    /// <param name="turmaId">Id da turma a ser validada</param>
    /// <param name="hash">Hash de acesso a ser verificado</param>
    /// <returns>Verdadeiro se o hash for válido, falso caso contrário</returns>
    Task<bool> ValidarHashAcesso(Guid turmaId, string hash);
}