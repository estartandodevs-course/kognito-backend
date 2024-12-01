<<<<<<< HEAD
using System;
using System.Threading;
using System.Threading.Tasks;
=======
using System.Data.Common;
>>>>>>> 892a573 (feat(turmas): implementa sistema de links de acesso para turmas)
using MediatR;
using Kognito.Turmas.Domain;
using Kognito.Turmas.Domain.Interfaces;
using Kognito.Turmas.App.Queries;
using FluentValidation.Results;
using Kognito.Turmas.App.ViewModels;
using EstartandoDevsCore.DomainObjects;

<<<<<<< HEAD
namespace Kognito.Turmas.App.Commands
=======
namespace Kognito.Turmas.App.Commands;

public class TurmaCommandHandler : CommandHandlerBase,
        IRequestHandler<CriarTurmaCommand, ValidationResult>,
        IRequestHandler<AtualizarTurmaCommand, ValidationResult>,
        IRequestHandler<ExcluirTurmaCommand, ValidationResult>,
        IRequestHandler<AtribuirProfessorCommand, ValidationResult>,
        IRequestHandler<SelecionarCorCommand, ValidationResult>,
        IRequestHandler<SelecionarIconesCommand, ValidationResult>,
        IRequestHandler<CriarAlunoNaTurmaCommand, ValidationResult>,
        IRequestHandler<ExcluirAlunoDaTurmaCommand, ValidationResult>,
        IRequestHandler<AtualizarStatusEnturmamentoCommand, ValidationResult>,
        IRequestHandler<GerarLinkTurmaCommand, ValidationResult>,        
        IRequestHandler<IngressarTurmaPorLinkCommand, ValidationResult>, 
        IAsyncDisposable
>>>>>>> 892a573 (feat(turmas): implementa sistema de links de acesso para turmas)
{
    public class TurmaCommandHandler : CommandHandler,
        IRequestHandler<CriarTurmaCommand, ValidationResult>,
        IRequestHandler<AtualizarTurmaCommand, ValidationResult>,
        IRequestHandler<ExcluirTurmaCommand, ValidationResult>,
        IRequestHandler<AtribuirProfessorCommand, ValidationResult>,
        IRequestHandler<SelecionarCorCommand, ValidationResult>,
        IRequestHandler<SelecionarIconesCommand, ValidationResult>,
        IDisposable
    {
<<<<<<< HEAD
        private readonly ITurmaRepository _turmaRepository;
        private readonly ITurmaQueries _turmaQueries;

        public TurmaCommandHandler(
            ITurmaRepository turmaRepository,
            ITurmaQueries turmaQueries)
=======
        _turmaRepository = turmaRepository ?? throw new ArgumentNullException(nameof(turmaRepository));
        _turmaQueries = turmaQueries ?? throw new ArgumentNullException(nameof(turmaQueries));
    }
        public async Task<ValidationResult> Handle(CriarTurmaCommand request, CancellationToken cancellationToken)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        try
>>>>>>> 892a573 (feat(turmas): implementa sistema de links de acesso para turmas)
        {
            _turmaRepository = turmaRepository;
            _turmaQueries = turmaQueries;
        }

        public async Task<ValidationResult> Handle(CriarTurmaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var turmas = await _turmaQueries.ObterTodasTurmas();
                if (turmas.Any(t => t.Nome == request.Nome))
                {
                    AdicionarErro("Já existe uma turma com este nome");
                    return ValidationResult;
                }

                var turma = new Turma(
                    request.Professor,
                    request.Nome,
                    request.Descricao,
                    request.Materia,
                    request.LinkAcesso,
                    Cor.Azul,
                    Icones.Primeiro
                );

                await _turmaRepository.Adicionar(turma);
                return await PersistirDados(_turmaRepository.UnitOfWork);
            }
            catch (Exception ex)
            {
                AdicionarErro($"Erro ao criar turma: {ex.Message}");
                return ValidationResult;
            }
<<<<<<< HEAD
=======

            var turma = new Turma
            (
                request.Professor ?? throw new ArgumentNullException(nameof(request.Professor)),
                request.Nome,
                request.Descricao,
                request.Materia,
                request.Cor,
                request.Icone
            );

            _turmaRepository.Adicionar(turma);
            return await PersistirDados(_turmaRepository.UnitOfWork);
>>>>>>> 892a573 (feat(turmas): implementa sistema de links de acesso para turmas)
        }

<<<<<<< HEAD
        public async Task<ValidationResult> Handle(AtualizarTurmaCommand request, CancellationToken cancellationToken)
=======
    public async Task<ValidationResult> Handle(GerarLinkTurmaCommand request, CancellationToken cancellationToken)
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

            var link = turma.GerarLinkDeAcesso(
                request.LimiteUsos ?? 0,
                request.DiasValidade ?? 7
            );

            _turmaRepository.Atualizar(turma);
            var result = await PersistirDados(_turmaRepository.UnitOfWork);
            
            if (result.IsValid)
            {
                var viewModel = LinkTurmaViewModel.FromDomain(link, turma.Id);
                AdicionarResultado(viewModel);
            }

            return ValidationResult;
        }
        catch (DbException)
        {
            AdicionarErro("Erro ao acessar o banco de dados");
            return ValidationResult;
        }
        catch (Exception ex)
        {
            AdicionarErro($"Erro ao gerar link de acesso: {ex.Message}");
            return ValidationResult;
        }
    }
        public async Task<ValidationResult> Handle(IngressarTurmaPorLinkCommand request, CancellationToken cancellationToken)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        try
        {
            var turma = await _turmaRepository.ObterTurmaCompletaPorId(request.TurmaId);
            if (turma == null)
            {
                AdicionarErro("Turma não encontrada");
                return ValidationResult;
            }

            var aluno = new Usuario("Aluno", request.AlunoId);

            try
            {
                turma.IngressarPorLink(aluno, request.Codigo);
            }
            catch (DomainException ex)
            {
                AdicionarErro(ex.Message);
                return ValidationResult;
            }

            _turmaRepository.Atualizar(turma);
            var result = await PersistirDados(_turmaRepository.UnitOfWork);

            if (result.IsValid)
            {
                var response = IngressarTurmaResponse.CriarSucesso(
                    turma.Id,
                    turma.Nome,
                    turma.Professor?.Nome ?? "Professor não definido",
                    turma.Materia
                );
                AdicionarResultado(response);
            }

            return ValidationResult;
        }
        catch (DbException)
        {
            AdicionarErro("Erro ao acessar o banco de dados");
            return ValidationResult;
        }
        catch (Exception ex)
        {
            AdicionarErro($"Erro ao ingressar na turma: {ex.Message}");
            return ValidationResult;
        }
    }

    public async Task<ValidationResult> Handle(AtualizarTurmaCommand request, CancellationToken cancellationToken)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        try
>>>>>>> 892a573 (feat(turmas): implementa sistema de links de acesso para turmas)
        {
            try
            {
                var turma = await _turmaRepository.ObterPorId(request.Id);
                if (turma == null)
                {
                    AdicionarErro("Turma não encontrada");
                    return ValidationResult;
                }

                var turmasExistentes = await _turmaQueries.ObterTodasTurmas();
                if (turmasExistentes.Any(t => t.Nome == request.Nome && t.Id != request.Id))
                {
                    AdicionarErro("Já existe uma turma com este nome");
                    return ValidationResult;
                }

                turma.AtribuirProfessor(request.Professor);
                turma.AtribuirNome(request.Nome);
                turma.AtribuirDescricao(request.Descricao);
                turma.AtribuirMateria(request.Materia);
                turma.AtribuirLinkAcesso(request.LinkAcesso);

                await _turmaRepository.Atualizar(turma);
                return await PersistirDados(_turmaRepository.UnitOfWork);
            }
            catch (Exception ex)
            {
                AdicionarErro($"Erro ao atualizar turma: {ex.Message}");
                return ValidationResult;
            }
        }

        public async Task<ValidationResult> Handle(ExcluirTurmaCommand request, CancellationToken cancellationToken)
        {
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

                await _turmaRepository.Remover(turma);
                return await PersistirDados(_turmaRepository.UnitOfWork);
            }
            catch (Exception ex)
            {
                AdicionarErro($"Erro ao excluir turma: {ex.Message}");
                return ValidationResult;
            }
<<<<<<< HEAD
        }

        public async Task<ValidationResult> Handle(AtribuirProfessorCommand request, CancellationToken cancellationToken)
=======

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
>>>>>>> 892a573 (feat(turmas): implementa sistema de links de acesso para turmas)
        {
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

                await _turmaRepository.Atualizar(turma);
                return await PersistirDados(_turmaRepository.UnitOfWork);
            }
            catch (Exception ex)
            {
                AdicionarErro($"Erro ao atribuir professor: {ex.Message}");
                return ValidationResult;
            }
        }

        public async Task<ValidationResult> Handle(SelecionarCorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var turma = await _turmaRepository.ObterPorId(request.TurmaId);
                if (turma == null)
                {
                    AdicionarErro("Turma não encontrada");
                    return ValidationResult;
                }

                turma.AtribuirCor(request.SelecionarCor);
                await _turmaRepository.Atualizar(turma);

                return await PersistirDados(_turmaRepository.UnitOfWork);
            }
            catch (Exception ex)
            {
                AdicionarErro($"Erro ao selecionar cor: {ex.Message}");
                return ValidationResult;
            }
        }

        public async Task<ValidationResult> Handle(SelecionarIconesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var turma = await _turmaRepository.ObterPorId(request.TurmaId);
                if (turma == null)
                {
                    AdicionarErro("Turma não encontrada");
                    return ValidationResult;
                }

                turma.AtribuirIcone(request.SelecionarIcones);
                await _turmaRepository.Atualizar(turma);

                return await PersistirDados(_turmaRepository.UnitOfWork);
            }
            catch (Exception ex)
            {
                AdicionarErro($"Erro ao selecionar ícone: {ex.Message}");
                return ValidationResult;
            }
        }

        public void Dispose()
        {
<<<<<<< HEAD
=======
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

            var aluno = new Usuario("Aluno", request.AlunoId);
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
>>>>>>> 892a573 (feat(turmas): implementa sistema de links de acesso para turmas)
            _turmaRepository?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}