using EstartandoDevsCore.Messages;
using Kognito.Tarefas.Domain;
using Kognito.Tarefas.Domain.interfaces;
using FluentValidation.Results;
using MediatR;
using Kognito.Tarefas.App.Events;

namespace Kognito.Tarefas.App.Commands;

public class TarefasCommandHandler : CommandHandler,
    IRequestHandler<CriarTarefaCommand, ValidationResult>,
    IRequestHandler<AtualizarTarefaCommand, ValidationResult>,
    IRequestHandler<EntregarTarefaCommand, ValidationResult>,
    IDisposable
{
    private readonly ITarefaRepository tarefaRepository;

    public TarefasCommandHandler(ITarefaRepository tarefaRepository)
    {
        this.tarefaRepository = tarefaRepository;
    }

    public async Task<ValidationResult> Handle(CriarTarefaCommand request, CancellationToken cancellationToken)
    {
        if (!request.EstaValido()) return request.ValidationResult;
        
        var tarefa = new Tarefa(request.Descricao, request.Conteudo, request.DataFinalEntrega, request.TurmaId);
        tarefaRepository.Adicionar(tarefa);
        
        return await PersistirDados(tarefaRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(AtualizarTarefaCommand request, CancellationToken cancellationToken)
    {
        if (!request.EstaValido()) return request.ValidationResult;
        
        var tarefa = await tarefaRepository.ObterPorIdAsync(request.Id);
        if (tarefa == null)
        {
            AdicionarErro("Tarefa não encontrada!");
            return ValidationResult;
        }

        tarefa.AtribuirDescricao(request.Descricao);
        tarefa.AtribuirConteudo(request.Conteudo);
        tarefa.AtribuirDataFinalEntrega(request.DataFinalEntrega);
        tarefa.AtribuirTurmaId(request.TurmaId);
        
        tarefaRepository.Atualizar(tarefa);
        
        return await PersistirDados(tarefaRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(EntregarTarefaCommand request, CancellationToken cancellationToken)
    {
        if (!request.EstaValido()) return request.ValidationResult;
        
        var tarefa = await tarefaRepository.ObterPorIdAsync(request.TarefaId);
        if (tarefa == null)
        {
            AdicionarErro("Tarefa não encontrada!");
            return ValidationResult;
        }

        var entrega = new Entrega(request.Conteudo, request.AlunoId, request.TarefaId);
        entrega.VerificarAtraso(tarefa.DataFinalEntrega);
        tarefa.AdicionarEntrega(entrega);

        var evento = new TarefaEntregueEvent(
            request.TarefaId,
            request.AlunoId,
            request.Conteudo,
            entrega.EntregueEm,
            entrega.Atrasada
        );
        
        tarefa.AdicionarEvento(evento);
        tarefaRepository.Atualizar(tarefa);
        
        return await PersistirDados(tarefaRepository.UnitOfWork);
    }

    public void Dispose()
    {
        tarefaRepository.Dispose();
    }
}