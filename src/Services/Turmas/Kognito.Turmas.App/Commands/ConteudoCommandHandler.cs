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
        IRequestHandler<ExcluirConteudo, ValidationResult>,
        IDisposable
    {
        private readonly IConteudoRepository _conteudoRepository;
        private readonly IConteudoQueries _conteudoQueries;

        public ConteudoCommandHandler(
            IConteudoRepository conteudoRepository,
            IConteudoQueries conteudoQueries)
        {
            _conteudoRepository = conteudoRepository;
            _conteudoQueries = conteudoQueries;
        }

        public async Task<ValidationResult> Handle(CriarConteudoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var conteudos = await _conteudoQueries.ObterTodosConteudos();
                if (conteudos.Any(c => c.Titulo == request.Titulo))
                {
                    AdicionarErro("Já existe um conteúdo com este título");
                    return ValidationResult;
                }

                var conteudo = new Conteudo(request.Titulo, request.ConteudoDidatico);
                await _conteudoRepository.Adicionar(conteudo);
                
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
            try
            {
                var conteudo = await _conteudoRepository.ObterPorId(request.Id);
                if (conteudo == null)
                {
                    AdicionarErro("Conteúdo não encontrado");
                    return ValidationResult;
                }

                conteudo.AtribuirTitulo(request.Titulo);
                conteudo.AtribuirConteudoDidatico(request.ConteudoDidatico);

                await _conteudoRepository.Atualizar(conteudo);
                return await PersistirDados(_conteudoRepository.UnitOfWork);
            }
            catch (Exception ex)
            {
                AdicionarErro($"Erro ao atualizar conteúdo: {ex.Message}");
                return ValidationResult;
            }
        }

        public async Task<ValidationResult> Handle(ExcluirConteudo request, CancellationToken cancellationToken)
        {
            try
            {
                var conteudo = await _conteudoRepository.ObterPorId(request.ConteudoId);
                if (conteudo == null)
                {
                    AdicionarErro("Conteúdo não encontrado");
                    return ValidationResult;
                }

                await _conteudoRepository.Remover(conteudo);
                return await PersistirDados(_conteudoRepository.UnitOfWork);
            }
            catch (Exception ex)
            {
                AdicionarErro($"Erro ao excluir conteúdo: {ex.Message}");
                return ValidationResult;
            }
        }

        public void Dispose()
        {
            _conteudoRepository?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}