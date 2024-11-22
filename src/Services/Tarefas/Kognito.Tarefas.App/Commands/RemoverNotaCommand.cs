using EstartandoDevsCore.Messages;

namespace Kognito.Tarefas.App.Commands;

public class RemoverNotaCommand : Command
{
    public Guid Id { get; private set; }

    public RemoverNotaCommand(Guid id)
    {
        Id = id;
    }
}