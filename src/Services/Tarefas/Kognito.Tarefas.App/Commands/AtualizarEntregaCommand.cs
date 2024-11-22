using EstartandoDevsCore.Messages;

namespace Kognito.Tarefas.App.Commands;

public class AtualizarEntregaCommand : Command
{
    public Guid Id { get; private set; }
    public string Conteudo { get; private set; }
    public Guid AlunoId { get; private set; }
    public Guid TarefaId { get; private set; }

    public AtualizarEntregaCommand(Guid id, string conteudo, Guid alunoId, Guid tarefaId)
    {
        Id = id;
        Conteudo = conteudo;
        AlunoId = alunoId;
        TarefaId = tarefaId;
    }
}