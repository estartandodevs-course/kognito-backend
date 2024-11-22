using EstartandoDevsCore.Messages;

namespace Kognito.Tarefas.App.Commands;

public class EntregarTarefaCommand : Command
{
    public string Conteudo { get; private set; }
    public Guid AlunoId { get; private set; }
    public Guid TarefaId { get; private set; }

    public EntregarTarefaCommand(string conteudo, Guid alunoId, Guid tarefaId)
    {
        Conteudo = conteudo;
        AlunoId = alunoId;
        TarefaId = tarefaId;
    }
}