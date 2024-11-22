using EstartandoDevsCore.Messages;

namespace Kognito.Tarefas.App.Commands;

public class CriarTarefaCommand : Command
{
    public string Descricao { get; private set; }
    public string Conteudo { get; private set; }
    public DateTime DataFinalEntrega { get; private set; }
    public Guid TurmaId { get; private set; }

    public CriarTarefaCommand(string descricao, string conteudo, DateTime dataFinalEntrega, Guid turmaId)
    {
        Descricao = descricao;
        Conteudo = conteudo;
        DataFinalEntrega = dataFinalEntrega;
        TurmaId = turmaId;
    }
}