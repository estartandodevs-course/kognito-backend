namespace Kognito.Tarefas.Domain.interfaces;

using EstartandoDevsCore.Data;

public interface INotaRepository : IRepository<Tarefa>, IDisposable
{
    Task<IEnumerable<Nota>> ObterPorAlunoAsync(Guid alunoId);
    Task<IEnumerable<Nota>> ObterPorTurmaAsync(Guid turmaId);    
}

