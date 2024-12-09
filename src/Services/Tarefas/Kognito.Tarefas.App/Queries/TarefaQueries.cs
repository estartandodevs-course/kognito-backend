using Kognito.Tarefas.App.ViewModels;
using Kognito.Tarefas.Domain.interfaces;
using Kognito.Usuarios.App.Domain;

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
        return tarefa != null ? TarefaViewModel.Mapear(tarefa) : null;
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

    public async Task<IEnumerable<TarefaViewModel>> ObterTarefasPorTurma(Guid turmaId)
    {
        var tarefas = await _tarefaRepository.ObterTarefasPorTurma(turmaId);
        return tarefas.Select(TarefaViewModel.Mapear);
    }
     public async Task<EntregaViewModel> ObterEntregaPorId(Guid entregaId)
    {
        var entrega = await _tarefaRepository.ObterEntregaPorIdAsync(entregaId);
        return entrega != null ? EntregaViewModel.Mapear(entrega) : null;
    }
    public async Task<IEnumerable<NotaViewModel>> ObterNotasPorTarefa(Guid tarefaId)
    {
        var tarefa = await _tarefaRepository.ObterPorIdAsync(tarefaId);
        if (tarefa == null) return Enumerable.Empty<NotaViewModel>();

        return tarefa.Entregas
            .SelectMany(e => e.Notas)
            .Select(NotaViewModel.Mapear);
    }
    
    public async Task<IEnumerable<EntregaViewModel>> ObterEntregasPorTarefa(Guid tarefaId)
    {
        var tarefa = await _tarefaRepository.ObterPorIdAsync(tarefaId);
        if (tarefa == null) return Enumerable.Empty<EntregaViewModel>();

        return tarefa.Entregas.Select(EntregaViewModel.Mapear);
    }
    
    public async Task<IEnumerable<TarefaViewModel>> ObterTarefasFiltradas(Guid turmaId, Neurodivergencia? neurodivergenciaAluno)
    {
        var tarefas = await _tarefaRepository.ObterTarefasFiltradas(turmaId, neurodivergenciaAluno);
        return tarefas.Select(TarefaViewModel.Mapear);
    }
} 