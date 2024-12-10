using Kognito.Usuarios.App.Domain;

namespace Kognito.Tarefas.Domain.interfaces;

using EstartandoDevsCore.Data;

public interface ITarefaRepository : IRepository<Tarefa>, IDisposable
{
    Task<IEnumerable<Tarefa>> ObterPorTurmaAsync(Guid turmaId);
    Task<IEnumerable<Tarefa>> ObterPorAlunoAsync(Guid alunoId);
    Task<Tarefa> ObterPorIdAsync(Guid id);
    Task<IEnumerable<Tarefa>> ObterTarefasPorTurma(Guid turmaId);
    Task<Entrega> ObterEntregaPorIdAsync(Guid entregaId);
    Task<IEnumerable<Tarefa>> ObterTarefasFiltradas(Guid turmaId, Neurodivergencia? neurodivergenciaAluno);
    Task<IEnumerable<Tarefa>> ObterTodasTarefasDoAluno(Guid alunoId);
}


