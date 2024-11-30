using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using EstartandoDevsCore.Messages;
using Kognito.Turmas.Domain;
using Kognito.Turmas.Domain.Interfaces;
using FluentValidation.Results;

namespace Kognito.Turmas.App.Commands
{
    public class EnturmamentoCommandHandler : CommandHandler,
        IRequestHandler<CriarAlunoNaTurmaCommand, ValidationResult>,
        IRequestHandler<ExcluirAlunoDaTurmaCommand, ValidationResult>,
        IRequestHandler<AtualizarStatusEnturmamentoCommand, ValidationResult>,
        IDisposable
    {
        private readonly ITurmaRepository _turmaRepository;

        public EnturmamentoCommandHandler(ITurmaRepository turmaRepository)
        {
            _turmaRepository = turmaRepository;
        }

        public async Task<ValidationResult> Handle(CriarAlunoNaTurmaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var turma = await _turmaRepository.ObterPorId(request.TurmaId);
                if (turma == null)
                {
                    AdicionarErro("Turma não encontrada");
                    return ValidationResult;
                }

                var aluno = new Usuario("Aluno", request.AlunoId);
                var enturmamento = new Enturmamento(aluno, turma, request.Status);
                
                turma.AdicionarEnturmamento(enturmamento);
                await _turmaRepository.Atualizar(turma);
                
                return await PersistirDados(_turmaRepository.UnitOfWork);
            }
            catch (Exception ex)
            {
                AdicionarErro($"Erro ao criar enturmamento: {ex.Message}");
                return ValidationResult;
            }
        }

        public async Task<ValidationResult> Handle(ExcluirAlunoDaTurmaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var turma = await _turmaRepository.ObterPorId(request.TurmaId);
                if (turma == null)
                {
                    AdicionarErro("Turma não encontrada");
                    return ValidationResult;
                }

                turma.RemoverEnturmamento(request.AlunoId);
                await _turmaRepository.Atualizar(turma);
                
                return await PersistirDados(_turmaRepository.UnitOfWork);
            }
            catch (Exception ex)
            {
                AdicionarErro($"Erro ao excluir aluno da turma: {ex.Message}");
                return ValidationResult;
            }
        }

        public async Task<ValidationResult> Handle(AtualizarStatusEnturmamentoCommand request, CancellationToken cancellationToken)
        {
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

                enturmamento.AtualizarStatus(request.Status);
                await _turmaRepository.Atualizar(turma);
                
                return await PersistirDados(_turmaRepository.UnitOfWork);
            }
            catch (Exception ex)
            {
                AdicionarErro($"Erro ao atualizar status do enturmamento: {ex.Message}");
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