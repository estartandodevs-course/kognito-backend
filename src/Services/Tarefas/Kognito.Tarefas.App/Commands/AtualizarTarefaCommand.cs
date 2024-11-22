using EstartandoDevsCore.Messages;

namespace Kognito.Tarefas.App.Commands;

public class AtualizarTarefaCommand : Command
{
    public Guid Id { get; private set; }
    public string Descricao { get; private set; }
    public string Conteudo { get; private set; }
    public DateTime DataFinalEntrega { get; private set; }
    public Guid TurmaId { get; private set; }

    public AtualizarTarefaCommand(Guid id, string descricao, string conteudo, DateTime dataFinalEntrega, Guid turmaId)
    {
        Id = id;
        Descricao = descricao;
        Conteudo = conteudo;
        DataFinalEntrega = dataFinalEntrega;
        TurmaId = turmaId;
    }
}