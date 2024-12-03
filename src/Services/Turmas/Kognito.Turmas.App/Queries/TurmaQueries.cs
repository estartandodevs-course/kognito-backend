using Kognito.Turmas.App.ViewModels;
using Kognito.Turmas.Domain.Interfaces;

namespace Kognito.Turmas.App.Queries;

public class TurmaQueries : ITurmaQueries
{
    private readonly ITurmaRepository _turmaRepository;
    private readonly IConteudoRepository _conteudoRepository;

    public TurmaQueries(ITurmaRepository turmaRepository, IConteudoRepository conteudoRepository)
    {
        _turmaRepository = turmaRepository;
        _conteudoRepository = conteudoRepository;
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
        var turmas = await _turmaRepository.ObterTodos();
        return turmas?.Select(TurmaViewModel.Mapear) 
            ?? Enumerable.Empty<TurmaViewModel>();
    }

    public async Task<IEnumerable<TurmaViewModel>> ObterTurmasPorProfessor(Guid professorId)
    {
        if (professorId == Guid.Empty)
            throw new ArgumentException("Id do professor inválido", nameof(professorId));

        var turmas = await _turmaRepository.ObterTurmasPorProfessor(professorId);
        return turmas?.Select(TurmaViewModel.Mapear) 
            ?? Enumerable.Empty<TurmaViewModel>();
    }

    public async Task<int> ObterQuantidadeAlunos(Guid turmaId)
    {
        if (turmaId == Guid.Empty)
            throw new ArgumentException("Id da turma inválido", nameof(turmaId));

        return await _turmaRepository.ObterQuantidadeAlunos(turmaId);
    }

    public async Task<TurmaAcessoViewModel> ObterAcessoTurma(Guid turmaId)
    {
        if (turmaId == Guid.Empty)
            throw new ArgumentException("Id da turma inválido", nameof(turmaId));

        var turma = await _turmaRepository.ObterPorId(turmaId);
        return TurmaAcessoViewModel.Mapear(turma);
    }
    public async Task<bool> VincularTurma(Guid conteudoId, Guid turmaId)
    {
        var conteudo = await _conteudoRepository.ObterPorId(conteudoId);
        if (conteudo == null) return false;

        var turma = await _turmaRepository.ObterPorId(turmaId);
        if (turma == null) return false;

        conteudo.VincularTurma(turma);
        _conteudoRepository.Atualizar(conteudo);

        return true;
    }

    public async Task<bool> ValidarHashAcesso(Guid turmaId, string hash)
    {
        if (turmaId == Guid.Empty || string.IsNullOrEmpty(hash))
            return false;

        var turma = await _turmaRepository.ObterPorId(turmaId);
        return turma != null && turma.HashAcesso == hash;
    }
}