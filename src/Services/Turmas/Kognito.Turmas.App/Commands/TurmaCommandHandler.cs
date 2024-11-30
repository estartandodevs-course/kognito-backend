using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using EstartandoDevsCore.Messages;
using Kognito.Turmas.Domain;
using Kognito.Turmas.Domain.Interfaces;
using Kognito.Turmas.App.Queries;
using FluentValidation.Results;

namespace Kognito.Turmas.App.Commands
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
        private readonly ITurmaRepository _turmaRepository;
        private readonly ITurmaQueries _turmaQueries;

        public TurmaCommandHandler(
            ITurmaRepository turmaRepository,
            ITurmaQueries turmaQueries)
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
        }

        public async Task<ValidationResult> Handle(AtualizarTurmaCommand request, CancellationToken cancellationToken)
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
        }

        public async Task<ValidationResult> Handle(AtribuirProfessorCommand request, CancellationToken cancellationToken)
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
            _turmaRepository?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}