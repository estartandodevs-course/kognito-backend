using EstartandoDevsCore.Messages;

namespace Kognito.Tarefas.App.Commands;

public class RemoverTarefaCommand : Command
{
    public Guid Id { get; private set; }

    public RemoverTarefaCommand(Guid id)
    {
        Id = id;
    }
}