using EstartandoDevsCore.Messages;
using Kognito.Turmas.Domain;

public class SelecionarIconesCommand : Command
{
    public Guid TurmaId { get; private set; }
    public Icones SelecionarIcones { get; private set; }

    public SelecionarIconesCommand(Guid turmaId, Icones icones)
    {
        ValidarTurmaId(turmaId);
        TurmaId = turmaId;
        SelecionarIcones = icones;
    }

    private void ValidarTurmaId(Guid turmaId)
    {
        if (turmaId == Guid.Empty)
            throw new ArgumentException("Id da turma inválido", nameof(turmaId));
    }
}