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
    public class TurmaHandler : CommandHandler,
        IRequestHandler<CriarTurmaCommand, FluentValidation.Results.ValidationResult>,
        IRequestHandler<AtualizarTurmaCommand, FluentValidation.Results.ValidationResult>,
        IRequestHandler<ExcluirTurmaCommand, FluentValidation.Results.ValidationResult>,
        IRequestHandler<AtribuirProfessorCommand, FluentValidation.Results.ValidationResult>,
        IRequestHandler<CriarConteudoCommand, FluentValidation.Results.ValidationResult>,
        IRequestHandler<AtualizarConteudoCommand, FluentValidation.Results.ValidationResult>,
        IRequestHandler<ExcluirConteudo, FluentValidation.Results.ValidationResult>,
        IRequestHandler<SelecionarCorCommand, FluentValidation.Results.ValidationResult>,
        IRequestHandler<SelecionarIconesCommand, FluentValidation.Results.ValidationResult>,
        IDisposable
    {
        private readonly ITurmaRepository _turmaRepository;
        private readonly IConteudoRepository _conteudoRepository;
        private readonly ITurmaQueries _turmaQueries;
        private readonly IConteudoQueries _conteudoQueries;

        public TurmaHandler(
            ITurmaRepository turmaRepository,
            IConteudoRepository conteudoRepository,
            ITurmaQueries turmaQueries,
            IConteudoQueries conteudoQueries)
        {
            _turmaRepository = turmaRepository;
            _conteudoRepository = conteudoRepository;
            _turmaQueries = turmaQueries;
            _conteudoQueries = conteudoQueries;
        }

        public async Task<ValidationResult> Handle(CriarTurmaCommand request, CancellationToken cancellationToken)
        {
            if (!request.EstaValido()) return request.ValidationResult;

            try
            {
                var turmas = await _turmaQueries.ObterTodasTurmas();
                if (turmas.Any(t => t.Nome == request.Nome))
                {
                    AdicionarErro("Já existe uma turma com este nome");
                    return ValidationResult;
                }

                var turma = new Turma(request.Professor, request.Nome, request.Descricao, 
                    request.Materia, request.LinkAcesso, null, null);
                
                await _turmaRepository.Adicionar(turma);
                return await PersistirDados(_turmaRepository.UnitOfWork);
            }
            catch (Exception ex)
            {
                AdicionarErro($"Erro ao criar turma: {ex.Message}");
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

                var professor = new Usuario("Nome do Professor", request.ProfessorId); // Ajuste conforme necessário
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

        public async Task<ValidationResult> Handle(AtualizarTurmaCommand request, CancellationToken cancellationToken)
        {
            if (!request.EstaValido()) return request.ValidationResult;

            try
            {
                var turma = await _turmaRepository.ObterPorId(request.Id);
                if (turma == null)
                {
                    AdicionarErro("Turma não encontrada");
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

                await _turmaRepository.Remover(turma);
                return await PersistirDados(_turmaRepository.UnitOfWork);
            }
            catch (Exception ex)
            {
                AdicionarErro($"Erro ao excluir turma: {ex.Message}");
                return ValidationResult;
            }
        }

        public async Task<ValidationResult> Handle(CriarConteudoCommand request, CancellationToken cancellationToken)
        {
            if (!request.EstaValido()) return request.ValidationResult;

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
            if (!request.EstaValido()) return request.ValidationResult;

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

        public async Task<ValidationResult> Handle(SelecionarCorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!Enum.IsDefined(typeof(EnumParaCores.Cor), request.SelecionarCor))
                {
                    AdicionarErro("Cor selecionada inválida");
                    return ValidationResult;
                }

                return await Task.FromResult(ValidationResult);
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
                if (!Enum.IsDefined(typeof(EnumParaIcones.Icones), request.SelecionarIcones))
                {
                    AdicionarErro("Ícone selecionado inválido");
                    return ValidationResult;
                }

                return await Task.FromResult(ValidationResult);
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
            _conteudoRepository?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}