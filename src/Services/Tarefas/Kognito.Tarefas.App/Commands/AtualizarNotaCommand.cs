using EstartandoDevsCore.Messages;

namespace Kognito.Tarefas.App.Commands;

public class AtualizarNotaCommand : Command
{
    public Guid Id { get; private set; }
    public string TituloTarefa { get; private set; }
    public double ValorNota { get; private set; }
    public Guid AlunoId { get; private set; }
    public Guid TurmaId { get; private set; }

    public AtualizarNotaCommand(Guid id, string tituloTarefa, double valorNota, Guid alunoId, Guid turmaId)
    {
        Id = id;
        TituloTarefa = tituloTarefa;
        ValorNota = valorNota;
        AlunoId = alunoId;
        TurmaId = turmaId;
    }
}