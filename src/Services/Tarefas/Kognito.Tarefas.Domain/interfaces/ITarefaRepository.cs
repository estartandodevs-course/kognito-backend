namespace Kognito.Tarefas.Domain.interfaces;

using EstartandoDevsCore.Data;

public interface ITarefaRepository : IRepository<Tarefa>, IDisposable
{
    Task<IEnumerable<Tarefa>> ObterPorTurmaAsync(Guid turmaId);
}