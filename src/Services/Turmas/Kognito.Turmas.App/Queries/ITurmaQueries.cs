using Kognito.Turmas.App.ViewModels;

namespace Kognito.Turmas.App.Queries;

public interface ITurmaQueries
{   
    /// <summary>
    /// Obtém uma turma pelo seu Id
    /// </summary>
    Task<TurmaViewModel> ObterPorId(Guid id);

    /// <summary>
    /// Obtém uma turma pelo hash de acesso
    /// </summary>
    Task<TurmaViewModel> ObterPorHashAcesso(string hashAcesso);

    /// <summary>
    /// Obtém todas as turmas de um professor específico
    /// </summary>
    Task<IEnumerable<TurmaViewModel>> ObterTurmasPorProfessor(Guid professorId);

    /// <summary>
    /// Obtém todas as turmas de um aluno específico
    /// </summary>
    Task<IEnumerable<TurmaViewModel>> ObterTurmasPorAluno(Guid alunoId);

    /// <summary>
    /// Obtém a quantidade de alunos em uma turma
    /// </summary>
    Task<int> ObterQuantidadeAlunos(Guid turmaId);

    /// <summary>
    /// Obtém as informações de acesso de uma turma
    /// </summary>
    Task<TurmaAcessoViewModel> ObterAcessoTurma(Guid turmaId);

    /// <summary>
    /// Vincula um conteúdo a uma turma
    /// </summary>
    Task<bool> VincularTurma(Guid conteudoId, Guid turmaId);
    
    /// <summary>
    /// Verifica se um usuário é o professor de uma turma específica
    /// </summary>
    Task<bool> VerificarProfessorTurma(Guid turmaId, Guid professorId);
    
    /// <summary>
    /// Obtém a lista de alunos de uma turma
    /// </summary>
    Task<IEnumerable<AlunoTurmaViewModel>> ObterAlunosTurma(Guid turmaId);
}