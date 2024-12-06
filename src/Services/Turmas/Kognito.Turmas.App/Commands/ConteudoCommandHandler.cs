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
    public class ConteudoCommandHandler : CommandHandler,
        IRequestHandler<CriarConteudoCommand, ValidationResult>,
        IRequestHandler<AtualizarConteudoCommand, ValidationResult>,
        IRequestHandler<ExcluirConteudoCommand, ValidationResult>,
        IRequestHandler<VincularConteudoTurmaCommand, ValidationResult>,
        IDisposable
    {
        private readonly IConteudoRepository _conteudoRepository;
        private readonly IConteudoQueries _conteudoQueries;
        private readonly ITurmaRepository _turmaRepository;

        public ConteudoCommandHandler(
            IConteudoRepository conteudoRepository,
            IConteudoQueries conteudoQueries,
            ITurmaRepository turmaRepository)
        {
            if (conteudoRepository == null)
                AdicionarErro("Repositório de conteúdo não pode ser nulo");
            if (conteudoQueries == null)
                AdicionarErro("Queries de conteúdo não pode ser nulo");
            if (turmaRepository == null)
                AdicionarErro("Repositório de turma não pode ser nulo");

            _conteudoRepository = conteudoRepository;
            _conteudoQueries = conteudoQueries;
            _turmaRepository = turmaRepository;
        }

public async Task<ValidationResult> Handle(CriarConteudoCommand request, CancellationToken cancellationToken)
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

        var conteudoResult = Conteudo.Criar(request.Titulo, request.ConteudoDidatico);
        if (!conteudoResult.Success)
        {
            foreach (var erro in conteudoResult.Errors)
                AdicionarErro(erro);
            return ValidationResult;
        }

        var conteudo = conteudoResult.Data;
        conteudo.AtribuirEntidadeId(request.Id);

        var vincularResult = conteudo.VincularTurma(turma);
        if (!vincularResult.Success)
        {
            AdicionarErro(vincularResult.Errors.First());
            return ValidationResult;
        }
        
        _conteudoRepository.Adicionar(conteudo);
        
        return await PersistirDados(_conteudoRepository.UnitOfWork);
    }
    catch (Exception ex)
    {
        AdicionarErro($"Erro ao criar conteúdo: {ex.Message}");
        return ValidationResult;
    }
}

        public async Task<ValidationResult> Handle(AtualizarConteudoCommand request, CancellationToken cancellationToken)
        {
            if (!request.EstaValido())
                return request.ValidationResult;

            try
            {
                var conteudo = await _conteudoRepository.ObterPorId(request.ConteudoId);
                if (conteudo == null)
                {
                    AdicionarErro("Conteúdo não encontrado");
                    return ValidationResult;
                }

                conteudo.AtribuirTitulo(request.Titulo);
                conteudo.AtribuirConteudoDidatico(request.ConteudoDidatico);

                _conteudoRepository.Atualizar(conteudo); 
                return await PersistirDados(_conteudoRepository.UnitOfWork);
            }
            catch (Exception ex)
            {
                AdicionarErro($"Erro ao atualizar conteúdo: {ex.Message}");
                return ValidationResult;
            }
        }

        public async Task<ValidationResult> Handle(ExcluirConteudoCommand request, CancellationToken cancellationToken)
        {
            if (!request.EstaValido())
                return request.ValidationResult;

            try
            {
                var conteudo = await _conteudoRepository.ObterPorId(request.ConteudoId);
                if (conteudo == null)
                {
                    AdicionarErro("Conteúdo não encontrado");
                    return ValidationResult;
                }

                _conteudoRepository.Apagar(c => c.Id == request.ConteudoId); 
                return await PersistirDados(_conteudoRepository.UnitOfWork);
            }
            catch (Exception ex)
            {
                AdicionarErro($"Erro ao excluir conteúdo: {ex.Message}");
                return ValidationResult;
            }
        }

        public async Task<ValidationResult> Handle(VincularConteudoTurmaCommand request, CancellationToken cancellationToken)
        {
            if (!request.EstaValido())
                return request.ValidationResult;

            try
            {
                var conteudo = await _conteudoRepository.ObterPorId(request.ConteudoId);
                if (conteudo == null)
                {
                    AdicionarErro("Conteúdo não encontrado");
                    return ValidationResult;
                }

                var turma = await _turmaRepository.ObterPorId(request.TurmaId);
                if (turma == null)
                {
                    AdicionarErro("Turma não encontrada");
                    return ValidationResult;
                }

                conteudo.VincularTurma(turma);
                _conteudoRepository.Atualizar(conteudo); 

                return await PersistirDados(_conteudoRepository.UnitOfWork);
            }
            catch (Exception ex)
            {
                AdicionarErro($"Erro ao vincular conteúdo à turma: {ex.Message}");
                return ValidationResult;
            }
        }

        public void Dispose()
        {
            _conteudoRepository?.Dispose();
            _turmaRepository?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}