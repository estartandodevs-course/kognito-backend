using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Kognito.Usuarios.App.Domain.Interface;

namespace Kognito.Tarefas.App.Events;

public class TarefaEntregueEventHandler : INotificationHandler<TarefaEntregueEvent>
{
    private readonly IServiceProvider _serviceProvider;

    public TarefaEntregueEventHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task Handle(TarefaEntregueEvent notification, CancellationToken cancellationToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var usuarioRepository = scope.ServiceProvider.GetRequiredService<IUsuariosRepository>();
            
            var usuario = await usuarioRepository.ObterPorId(notification.AlunoId);
            
            if (usuario == null) return;
            
            if (notification.Atrasada)
                usuario.ResetarOfensiva();
            else
                usuario.AcrescentarOfensiva();
            
            usuarioRepository.Atualizar(usuario);
            
            await usuarioRepository.UnitOfWork.Commit();
        }
    }
}