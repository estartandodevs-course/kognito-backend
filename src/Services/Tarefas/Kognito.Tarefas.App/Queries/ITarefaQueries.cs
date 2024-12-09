﻿using Kognito.Tarefas.App.ViewModels;
using Kognito.Usuarios.App.Domain;

namespace Kognito.Tarefas.App.Queries;

public interface ITarefaQueries
{
    Task<TarefaViewModel> ObterPorId(Guid id);
    Task<IEnumerable<TarefaViewModel>> ObterPorTurma(Guid turmaId);
    Task<IEnumerable<TarefaViewModel>> ObterPorAluno(Guid alunoId);
    Task<IEnumerable<TarefaViewModel>> ObterTarefasPorTurma(Guid turmaId);
    Task<EntregaViewModel> ObterEntregaPorId(Guid entregaId);
    Task<IEnumerable<NotaViewModel>> ObterNotasPorTarefa(Guid tarefaId);
    Task<IEnumerable<EntregaViewModel>> ObterEntregasPorTarefa(Guid tarefaId);
    Task<IEnumerable<TarefaViewModel>> ObterTarefasFiltradas(Guid turmaId, Neurodivergencia? neurodivergenciaAluno);
}