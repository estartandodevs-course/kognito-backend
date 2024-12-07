using Kognito.Tarefas.App.ViewModels;
using Kognito.Tarefas.Domain.interfaces;

namespace Kognito.Tarefas.App.Queries;

public class TarefaQueries : ITarefaQueries
{
    private readonly ITarefaRepository _tarefaRepository;

    public TarefaQueries(ITarefaRepository tarefaRepository)
    {
        _tarefaRepository = tarefaRepository;
    }

    public async Task<TarefaViewModel> ObterPorId(Guid id)
    {
        var tarefa = await _tarefaRepository.ObterPorIdAsync(id);
        return TarefaViewModel.Mapear(tarefa);
    }

    public async Task<IEnumerable<TarefaViewModel>> ObterPorTurma(Guid turmaId)
    {
        var tarefas = await _tarefaRepository.ObterPorTurmaAsync(turmaId);
        return tarefas.Select(TarefaViewModel.Mapear);
    }

    public async Task<IEnumerable<TarefaViewModel>> ObterPorAluno(Guid alunoId)
    {
        var tarefas = await _tarefaRepository.ObterPorAlunoAsync(alunoId);
        return tarefas.Select(TarefaViewModel.Mapear);
    }

    public async Task<IEnumerable<TarefaViewModel>> ObterTarefasComNotasPorTurma(Guid turmaId)
    {
        var tarefas = await _tarefaRepository.ObterTarefasComNotasPorTurmaAsync(turmaId);
        return tarefas.Select(TarefaViewModel.Mapear);
    }
     public async Task<EntregaViewModel> ObterEntregaPorId(Guid entregaId)
    {
        var entrega = await _tarefaRepository.ObterEntregaPorIdAsync(entregaId);
        return entrega != null ? EntregaViewModel.Mapear(entrega) : null;
    }
} 