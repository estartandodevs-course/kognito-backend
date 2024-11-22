using EstartandoDevsCore.Messages;

namespace Kognito.Tarefas.App.Commands;

public class AtribuirNotaCommand : Command
{
    public string TituloTarefa { get; private set; }
    public double ValorNota { get; private set; }
    public Guid AlunoId { get; private set; }
    public Guid TurmaId { get; private set; }

    public AtribuirNotaCommand(string tituloTarefa, double valorNota, Guid alunoId, Guid turmaId)
    {
        TituloTarefa = tituloTarefa;
        ValorNota = valorNota;
        AlunoId = alunoId;
        TurmaId = turmaId;
    }
}