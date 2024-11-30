using System;
using Kognito.Turmas.App.Queries;
using Kognito.Turmas.App.ViewModels;
using Kognito.Turmas.Domain.Interfaces;
using Kognito.Turmas.Domain;
using System.Linq;

namespace Kognito.Turmas.App.Queries;

public class TurmaQueries : ITurmaQueries
{
   private readonly ITurmaRepository _turmaRepository;
    public TurmaQueries(ITurmaRepository turmaRepository)
    {
        _turmaRepository = turmaRepository;
    }
    public async Task<IEnumerable<TurmaViewModel>> ObterPorId(Guid id)
    {
        
        if (id == Guid.Empty)
            throw new ArgumentException("Id da turma inválido", nameof(id));

        var turma = await _turmaRepository.ObterPorId(id);
        return turma == null 
            ? Enumerable.Empty<TurmaViewModel>() 
            : new List<TurmaViewModel> { TurmaViewModel.Mapear(turma) };
    }

    public async Task<IEnumerable<TurmaViewModel>> ObterTodasTurmas()
    {
     var turmas = await _turmaRepository.ObterTurmas();
        return turmas?.Select(turma => TurmaViewModel.Mapear(turma)) 
            ?? Enumerable.Empty<TurmaViewModel>();;
    }

    public async Task<IEnumerable<TurmaViewModel>> ObterTurmasPorProfessor(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id do professor inválido", nameof(id));

        var turmas = await _turmaRepository.ObterTurmas();
        return turmas?
            .Where(turma => turma.Professor != null && turma.Professor.Id == id)
            .Select(turma => TurmaViewModel.Mapear(turma)) 
            ?? Enumerable.Empty<TurmaViewModel>();
    }

    
}
