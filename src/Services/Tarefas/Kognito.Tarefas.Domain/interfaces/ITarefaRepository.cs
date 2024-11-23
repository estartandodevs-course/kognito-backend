namespace Kognito.Tarefas.Domain.interfaces;

using EstartandoDevsCore.Data;

public interface ITarefaRepository : IRepository<Tarefa>, IDisposable
{
    Task<IEnumerable<Tarefa>> ObterPorTurmaAsync(Guid turmaId);
    Task<IEnumerable<Tarefa>> ObterPorAlunoAsync(Guid alunoId);
    Task<IEnumerable<Tarefa>> ObterTarefasComNotasPorTurmaAsync(Guid turmaId);
}