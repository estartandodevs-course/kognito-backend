using Kognito.Tarefas.App.ViewModels;
using Kognito.Tarefas.Domain;
using Kognito.Tarefas.Domain.interfaces;
using Kognito.Turmas.App.Queries;
using Kognito.Usuarios.App.Domain;
using Kognito.Usuarios.App.Queries;

namespace Kognito.Tarefas.App.Queries;

public class TarefaQueries : ITarefaQueries
{
    private readonly ITarefaRepository _tarefaRepository;
    private readonly ITurmaQueries _turmaQueries;
    private readonly IUsuarioQueries _usuarioQueries;

    public TarefaQueries(ITarefaRepository tarefaRepository, ITurmaQueries turmaQueries, IUsuarioQueries usuarioQueries)
    {
        _tarefaRepository = tarefaRepository;
        _turmaQueries = turmaQueries;
        _usuarioQueries = usuarioQueries;
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
    
    public async Task<DesempenhoViewModel> ObterDesempenhoAluno(Guid alunoId)
    {
        var usuario = await _usuarioQueries.ObterPorId(alunoId);
        var turmas = await _turmaQueries.ObterTurmasPorAluno(alunoId);
        var todasTarefas = new List<Tarefa>();

        foreach (var turma in turmas)
        {
            var tarefasTurma = await _tarefaRepository.ObterTarefasPorTurma(turma.Id);
            var tarefasFiltradas = tarefasTurma.Where(t => 
                !t.NeurodivergenciasAlvo.Any() || 
                (usuario.Neurodivergencia.HasValue && t.NeurodivergenciasAlvo.Contains(usuario.Neurodivergencia.Value)));
            
            todasTarefas.AddRange(tarefasFiltradas);
        }

        var totalTarefas = todasTarefas.Count;
        var entregasNoTempo = todasTarefas.Count(t => 
            t.Entregas.Any(e => e.AlunoId == alunoId && !e.Atrasada));
        
        var entregasAtrasadas = todasTarefas.Count(t => 
            t.Entregas.Any(e => e.AlunoId == alunoId && e.Atrasada));
        
        var tarefasPendentes = todasTarefas.Count(t => 
            !t.Entregas.Any(e => e.AlunoId == alunoId));

        return new DesempenhoViewModel
        {
            TotalAssignments = totalTarefas,
            SubmitedAssignments = entregasNoTempo,
            lateAssignments = entregasAtrasadas,
            PendingAssignments = tarefasPendentes
        };
    }

    public async Task<DesempenhoViewModel> ObterDesempenhoTurma(Guid turmaId, Guid alunoId)
    {
        var tarefas = await _tarefaRepository.ObterTarefasPorTurma(turmaId);
        var hoje = DateTime.Now;
        var usuario = await _usuarioQueries.ObterPorId(alunoId);
        
        var tarefasDisponiveis = tarefas.Where(t => 
            !t.NeurodivergenciasAlvo.Any() || 
            t.NeurodivergenciasAlvo.Contains(usuario.Neurodivergencia.Value));

        return new DesempenhoViewModel
        {
            TotalAssignments = tarefasDisponiveis.Count(),
            SubmitedAssignments = tarefasDisponiveis.Count(t => 
                t.Entregas.Any(e => e.AlunoId == alunoId && !e.Atrasada)),
            lateAssignments = tarefasDisponiveis.Count(t => 
                t.Entregas.Any(e => e.AlunoId == alunoId && e.Atrasada)),
            PendingAssignments = tarefasDisponiveis.Count(t => 
                !t.Entregas.Any(e => e.AlunoId == alunoId))
        };
    }
} 