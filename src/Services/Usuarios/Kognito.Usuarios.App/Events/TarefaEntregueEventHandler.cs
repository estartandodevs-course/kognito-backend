using EstartandoDevsCore.Messages;
using EstartandoDevsCore.Mediator;
using Kognito.Tarefas.App.Events;
using Kognito.Usuarios.App.Commands;
using Kognito.Usuarios.App.Domain.Interface;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Kognito.Usuarios.App.Events;

public class TarefaEntregueEventHandler : INotificationHandler<TarefaEntregueEvent>
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IMediatorHandler _mediatorHandler;

    public TarefaEntregueEventHandler(
        IServiceProvider serviceProvider,
        IMediatorHandler mediatorHandler)
    {
        _serviceProvider = serviceProvider;
        _mediatorHandler = mediatorHandler;
    }

    public async Task Handle(TarefaEntregueEvent notification, CancellationToken cancellationToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            try
            {
                // repositórios necessários
                var usuarioRepository = scope.ServiceProvider.GetRequiredService<IUsuariosRepository>();
                var emblemaRepository = scope.ServiceProvider.GetRequiredService<IEmblemaRepository>();
                
                var usuario = await usuarioRepository.ObterPorId(notification.AlunoId);
                if (usuario == null) return;

                if (notification.Atrasada)
                    usuario.ResetarOfensiva();
                else
                    usuario.AcrescentarOfensiva();

                usuarioRepository.Atualizar(usuario);

                // lógica para emblemas
                var quantidadeEntregas = await emblemaRepository
                    .ObterQuantidadeEntregasUsuario(notification.AlunoId);

                // Envia comando para verificar e desbloquear emblema
                await _mediatorHandler.EnviarComando(
                    new DesbloquearEmblemaCommand(notification.AlunoId, quantidadeEntregas));

                // Commit das alterações
                await usuarioRepository.UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao processar evento de tarefa entregue", ex);
            }
        }
    }
}