using EstartandoDevsCore.Messages;

namespace Kognito.Tarefas.App.Commands;

public class RemoverEntregaCommand : Command
{
    public Guid Id { get; private set; }

    public RemoverEntregaCommand(Guid id)
    {
        Id = id;
    }
}