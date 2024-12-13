using System;
using System.Data.Common;
using MediatR;
using EstartandoDevsCore.Messages;
using Kognito.Turmas.Domain;
using Kognito.Turmas.Domain.Interfaces;
using Kognito.Turmas.App.Queries;
using FluentValidation.Results;
using Kognito.Usuarios.App.Queries;

namespace Kognito.Turmas.App.Commands;

public class TurmaCommandHandler : CommandHandler,
    IRequestHandler<CriarTurmaCommand, ValidationResult>,
    IRequestHandler<AtualizarTurmaCommand, ValidationResult>,
    IRequestHandler<ExcluirTurmaCommand, ValidationResult>,
    IRequestHandler<AtribuirProfessorCommand, ValidationResult>,
    IRequestHandler<SelecionarCorCommand, ValidationResult>,
    IRequestHandler<SelecionarIconesCommand, ValidationResult>,
    IRequestHandler<CriarAlunoNaTurmaCommand, ValidationResult>,
    IRequestHandler<ExcluirAlunoDaTurmaCommand, ValidationResult>,
    IRequestHandler<AtualizarStatusEnturmamentoCommand, ValidationResult>,
    IAsyncDisposable
{
    private readonly ITurmaRepository _turmaRepository;
    private readonly ITurmaQueries _turmaQueries;
    private readonly IUsuarioQueries _usuarioQueries;

    public TurmaCommandHandler(
        ITurmaRepository turmaRepository,
        IUsuarioQueries usuarioQueries,
        ITurmaQueries turmaQueries)
    
    {
        _turmaRepository = turmaRepository;
        _turmaQueries = turmaQueries;
        _usuarioQueries = usuarioQueries;
    }

    public async Task<ValidationResult> Handle(CriarTurmaCommand request, CancellationToken cancellationToken)
    {
        if (!request.EstaValido())
            return request.ValidationResult;

        try
        {
            var professorData = await _usuarioQueries.ObterPorId(request.ProfessorId);
            if (professorData == null)
            {
                AdicionarErro("Professor não encontrado");
                return ValidationResult;
            }

            var professor = new Usuario(professorData.Name, request.ProfessorId);
            
            var turma = new Turma(
                professor,
                request.Nome,
                request.Descricao,
                request.Materia,
                request.Cor,
                request.Icone
            );

            _turmaRepository.Adicionar(turma);
            return await PersistirDados(_turmaRepository.UnitOfWork);
        }
        catch (Exception ex)
        {
            AdicionarErro($"Erro ao criar turma: {ex.Message}");
            return ValidationResult;
        }
    }

    public async Task<ValidationResult> Handle(AtualizarTurmaCommand request, CancellationToken cancellationToken)
    {
        if (!request.EstaValido())
            return request.ValidationResult;

        try
        {
            var turma = await _turmaRepository.ObterPorId(request.Id);
            if (turma == null)
            {
                AdicionarErro("Turma não encontrada");
                return ValidationResult;
            }

            turma.AtribuirNome(request.Nome);
            turma.AtribuirDescricao(request.Descricao);
            turma.AtribuirMateria(request.Materia);

            _turmaRepository.Atualizar(turma);
            return await PersistirDados(_turmaRepository.UnitOfWork);
        }
        catch (DbException)
        {
            AdicionarErro("Erro ao acessar o banco de dados");
            return ValidationResult;
        }
        catch (Exception)
        {
            AdicionarErro("Ocorreu um erro interno ao atualizar a turma");
            return ValidationResult;
        }
    }

    public async Task<ValidationResult> Handle(ExcluirTurmaCommand request, CancellationToken cancellationToken)
    {
        if (!request.EstaValido())
            return request.ValidationResult;

        try
        {
            var turma = await _turmaRepository.ObterPorId(request.TurmaId);
            if (turma == null)
            {
                AdicionarErro("Turma não encontrada");
                return ValidationResult;
            }

            if (turma.Enturmamentos.Any())
            {
                AdicionarErro("Não é possível excluir uma turma que possui alunos matriculados");
                return ValidationResult;
            }

            _turmaRepository.Apagar(t => t.Id == request.TurmaId);
            return await PersistirDados(_turmaRepository.UnitOfWork);
        }
        catch (DbException)
        {
            AdicionarErro("Erro ao acessar o banco de dados");
            return ValidationResult;
        }
        catch (Exception)
        {
            AdicionarErro("Ocorreu um erro interno ao excluir a turma");
            return ValidationResult;
        }
    }

    public async Task<ValidationResult> Handle(AtribuirProfessorCommand request, CancellationToken cancellationToken)
    {
        if (!request.EstaValido())
            return request.ValidationResult;

        try
        {
            var turma = await _turmaRepository.ObterPorId(request.TurmaId);
            if (turma == null)
            {
                AdicionarErro("Turma não encontrada");
                return ValidationResult;
            }

            var professor = new Usuario("Professor", request.ProfessorId);
            turma.AtribuirProfessor(professor);

            _turmaRepository.Atualizar(turma);
            return await PersistirDados(_turmaRepository.UnitOfWork);
        }
        catch (DbException)
        {
            AdicionarErro("Erro ao acessar o banco de dados");
            return ValidationResult;
        }
        catch (Exception)
        {
            AdicionarErro("Ocorreu um erro interno ao atribuir professor");
            return ValidationResult;
        }
    }

    public async Task<ValidationResult> Handle(SelecionarCorCommand request, CancellationToken cancellationToken)
    {
        if (!request.EstaValido())
            return request.ValidationResult;

        try
        {
            var turma = await _turmaRepository.ObterPorId(request.TurmaId);
            if (turma == null)
            {
                AdicionarErro("Turma não encontrada");
                return ValidationResult;
            }

            turma.AtribuirCor(request.SelecionarCor);
            _turmaRepository.Atualizar(turma);

            return await PersistirDados(_turmaRepository.UnitOfWork);
        }
        catch (DbException)
        {
            AdicionarErro("Erro ao acessar o banco de dados");
            return ValidationResult;
        }
        catch (Exception)
        {
            AdicionarErro("Ocorreu um erro interno ao selecionar cor");
            return ValidationResult;
        }
    }

    public async Task<ValidationResult> Handle(SelecionarIconesCommand request, CancellationToken cancellationToken)
    {
        if (!request.EstaValido())
            return request.ValidationResult;

        try
        {
            var turma = await _turmaRepository.ObterPorId(request.TurmaId);
            if (turma == null)
            {
                AdicionarErro("Turma não encontrada");
                return ValidationResult;
            }

            turma.AtribuirIcone(request.SelecionarIcones);
            _turmaRepository.Atualizar(turma);

            return await PersistirDados(_turmaRepository.UnitOfWork);
        }
        catch (DbException)
        {
            AdicionarErro("Erro ao acessar o banco de dados");
            return ValidationResult;
        }
        catch (Exception)
        {
            AdicionarErro("Ocorreu um erro interno ao selecionar ícone");
            return ValidationResult;
        }
    }

    public async Task<ValidationResult> Handle(CriarAlunoNaTurmaCommand request, CancellationToken cancellationToken)
    {
        if (!request.EstaValido())
            return request.ValidationResult;

        try
        {
            var turma = await _turmaRepository.ObterPorId(request.TurmaId);
            if (turma == null)
            {
                AdicionarErro("Turma não encontrada");
                return ValidationResult;
            }

            var alunoData = await _usuarioQueries.ObterPorId(request.AlunoId);
            if (alunoData == null)
            {
                AdicionarErro("Aluno não encontrado");
                return ValidationResult;
            }

            var aluno = new Usuario(alunoData.Name, request.AlunoId);
            var enturmamentoResult = Enturmamento.Criar(aluno, turma, request.Status);
        
            if (!enturmamentoResult.Success)
            {
                foreach (var erro in enturmamentoResult.Errors)
                    AdicionarErro(erro);
                return ValidationResult;
            }

            var resultado = turma.AdicionarEnturmamento(enturmamentoResult.Data);

            if (!resultado.Success)
            {
                foreach (var resultadoError in resultado.Errors)
                {
                    AdicionarErro(resultadoError);
                }
            }
            _turmaRepository.Atualizar(turma);
        
            return await PersistirDados(_turmaRepository.UnitOfWork);
        }
        catch (DbException)
        {
            AdicionarErro("Erro ao acessar o banco de dados");
            return ValidationResult;
        }
        catch (Exception ex)
        {
            AdicionarErro($"Ocorreu um erro interno ao criar enturmamento: {ex.Message}");
            return ValidationResult;
        }
    }

    public async Task<ValidationResult> Handle(ExcluirAlunoDaTurmaCommand request, CancellationToken cancellationToken)
{
    if (!request.EstaValido())
        return request.ValidationResult;

    try
    {
        var turma = await _turmaRepository.ObterPorId(request.TurmaId);
        if (turma == null)
        {
            AdicionarErro("Turma não encontrada");
            return ValidationResult;
        }

        var enturmamento = turma.Enturmamentos.FirstOrDefault(e => e.Aluno.Id == request.AlunoId);
        if (enturmamento == null)
        {
            AdicionarErro("Aluno não encontrado na turma especificada");
            return ValidationResult;
        }

        var result = turma.RemoverEnturmamento(request.AlunoId);
        if (!result.Success)
        {
            foreach (var erro in result.Errors)
                AdicionarErro(erro);
            return ValidationResult;
        }

        _turmaRepository.Atualizar(turma);
        return await PersistirDados(_turmaRepository.UnitOfWork);
    }
    catch (DbException)
    {
        AdicionarErro("Erro ao acessar o banco de dados");
        return ValidationResult;
    }
    catch (Exception)
    {
        AdicionarErro("Ocorreu um erro interno ao excluir aluno da turma");
        return ValidationResult;
    }
}

    public async Task<ValidationResult> Handle(AtualizarStatusEnturmamentoCommand request, CancellationToken cancellationToken)
    {
        if (!request.EstaValido())
            return request.ValidationResult;

        try
        {
            var turma = await _turmaRepository.ObterPorId(request.TurmaId);
            if (turma == null)
            {
                AdicionarErro("Turma não encontrada");
                return ValidationResult;
            }

            var enturmamento = turma.Enturmamentos.FirstOrDefault(e => e.Aluno.Id == request.AlunoId);
            if (enturmamento == null)
            {
                AdicionarErro("Aluno não encontrado na turma");
                return ValidationResult;
            }

            var statusResult = enturmamento.AtualizarStatus(request.Status);
            if (!statusResult.Success)
            {
                foreach (var erro in statusResult.Errors)
                    AdicionarErro(erro);
                return ValidationResult;
            }

            _turmaRepository.Atualizar(turma);
            
            return await PersistirDados(_turmaRepository.UnitOfWork);
        }
        catch (DbException)
        {
            AdicionarErro("Erro ao acessar o banco de dados");
            return ValidationResult;
        }
        catch (Exception)
        {
            AdicionarErro("Ocorreu um erro interno ao atualizar status do enturmamento");
            return ValidationResult;
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_turmaRepository is IAsyncDisposable asyncDisposableRepo)
            await asyncDisposableRepo.DisposeAsync();
        else
            _turmaRepository?.Dispose();

        if (_turmaQueries is IAsyncDisposable asyncDisposableQueries)
            await asyncDisposableQueries.DisposeAsync();
        else if (_turmaQueries is IDisposable disposableQueries)
            disposableQueries.Dispose();

        GC.SuppressFinalize(this);
    }
}