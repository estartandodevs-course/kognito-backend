using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using EstartandoDevsCore.Messages;
using Kognito.Turmas.Domain;
using Kognito.Turmas.Domain.Interfaces;
using Kognito.Turmas.App.Queries;
using FluentValidation.Results;

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

    public TurmaCommandHandler(
        ITurmaRepository turmaRepository,
        ITurmaQueries turmaQueries)
    {
        _turmaRepository = turmaRepository ?? throw new ArgumentNullException(nameof(turmaRepository));
        _turmaQueries = turmaQueries ?? throw new ArgumentNullException(nameof(turmaQueries));
    }

    public async Task<ValidationResult> Handle(CriarTurmaCommand request, CancellationToken cancellationToken)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        try
        {

            var turma = new Turma(
                id:request.Id,
                professor: request.Professor ?? throw new ArgumentNullException(nameof(request.Professor)),
                nome: request.Nome,
                descricao: request.Descricao,
                materia: request.Materia,
                cor: request.Cor,
                icones: request.Icone 
            );

            _turmaRepository.Adicionar(turma);
            return await PersistirDados(_turmaRepository.UnitOfWork);
            
        }
        catch (DbException)
        {
            AdicionarErro("Erro ao acessar o banco de dados");
            return ValidationResult;
        }
        catch (Exception)
        {
            AdicionarErro("Ocorreu um erro interno ao criar a turma");
            return ValidationResult;
        }
    }

    public async Task<ValidationResult> Handle(AtualizarTurmaCommand request, CancellationToken cancellationToken)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        try
        {
            var turma = await _turmaRepository.ObterPorId(request.Id);
            if (turma == null)
            {
                AdicionarErro("Turma não encontrada");
                return ValidationResult;
            }


            turma.AtribuirProfessor(request.Professor ?? throw new ArgumentNullException(nameof(request.Professor)));
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
        if (request == null) throw new ArgumentNullException(nameof(request));

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
        if (request == null) throw new ArgumentNullException(nameof(request));

        try
        {
            var turma = await _turmaRepository.ObterPorId(request.TurmaId);
            if (turma == null)
            {
                AdicionarErro("Turma não encontrada");
                return ValidationResult;
            }

            if (request.ProfessorId == Guid.Empty)
            {
                AdicionarErro("ID do professor inválido");
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
        if (request == null) throw new ArgumentNullException(nameof(request));

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
        if (request == null) throw new ArgumentNullException(nameof(request));

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
        if (request == null) throw new ArgumentNullException(nameof(request));

        try
        {
            var turma = await _turmaRepository.ObterPorId(request.TurmaId);
            if (turma == null)
            {
                AdicionarErro("Turma não encontrada");
                return ValidationResult;
            }

            if (request.AlunoId == Guid.Empty)
            {
                AdicionarErro("ID do aluno inválido");
                return ValidationResult;
            }

            var aluno = new Usuario(request.AlunoNome, request.AlunoId);
            var enturmamento = new Enturmamento(aluno, turma, request.Status);
            
            turma.AdicionarEnturmamento(enturmamento);
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
            AdicionarErro("Ocorreu um erro interno ao criar enturmamento");
            return ValidationResult;
        }
    }

    public async Task<ValidationResult> Handle(ExcluirAlunoDaTurmaCommand request, CancellationToken cancellationToken)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        try
        {
            var turma = await _turmaRepository.ObterPorId(request.TurmaId);
            if (turma == null)
            {
                AdicionarErro("Turma não encontrada");
                return ValidationResult;
            }

            if (request.AlunoId == Guid.Empty)
            {
                AdicionarErro("ID do aluno inválido");
                return ValidationResult;
            }

            turma.RemoverEnturmamento(request.AlunoId);
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
        if (request == null) throw new ArgumentNullException(nameof(request));

        try
        {
            var turma = await _turmaRepository.ObterPorId(request.TurmaId);
            if (turma == null)
            {
                AdicionarErro("Turma não encontrada");
                return ValidationResult;
            }

            if (request.AlunoId == Guid.Empty)
            {
                AdicionarErro("ID do aluno inválido");
                return ValidationResult;
            }

            var enturmamento = turma.Enturmamentos.FirstOrDefault(e => e.Aluno.Id == request.AlunoId);
            if (enturmamento == null)
            {
                AdicionarErro("Aluno não encontrado na turma");
                return ValidationResult;
            }

            enturmamento.AtualizarStatus(request.Status);
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