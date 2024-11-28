using Kognito.Tarefas.App.ViewModels;

namespace Kognito.Tarefas.App.Queries;

public interface ITarefaQueries
{
    Task<TarefaViewModel> ObterPorId(Guid id);
    Task<IEnumerable<TarefaViewModel>> ObterPorTurma(Guid turmaId);
    Task<IEnumerable<TarefaViewModel>> ObterPorAluno(Guid alunoId);
    Task<IEnumerable<TarefaViewModel>> ObterTarefasComNotasPorTurma(Guid turmaId);
}