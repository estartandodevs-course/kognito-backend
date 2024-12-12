using System;
using System.Threading;
using System.Threading.Tasks;
using EstartandoDevsCore.Mediator;
using Kognito.Tarefas.App.Events;
using Kognito.Usuarios.App.Commands;
using Kognito.Usuarios.App.Domain.Interface;
using MediatR;

namespace Kognito.Usuarios.App.Events;

public class TarefaEntregueEventHandler : INotificationHandler<TarefaEntregueEvent>
{
    private readonly IMediatorHandler _mediatorHandler;
    private readonly IEmblemaRepository _emblemaRepository;

    public TarefaEntregueEventHandler(
        IMediatorHandler mediatorHandler,
        IEmblemaRepository emblemaRepository)
    {
        _mediatorHandler = mediatorHandler;
        _emblemaRepository = emblemaRepository;
    }
    
    public async Task Handle(TarefaEntregueEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            var quantidadeEntregas = await _emblemaRepository
                .ObterQuantidadeEntregasUsuario(notification.UsuarioId);
                
            await _mediatorHandler.EnviarComando(
                new DesbloquearEmblemaCommand(notification.UsuarioId, quantidadeEntregas));
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao processar evento de tarefa entregue", ex);
        }
    }
}