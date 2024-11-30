using EstartandoDevsCore.Messages;
using Kognito.Turmas.Domain;

public class SelecionarCorCommand : Command
{
    public Guid TurmaId { get; private set; }
    public Cor SelecionarCor { get; private set; }

    public SelecionarCorCommand(Guid turmaId, Cor cor)
    {
        ValidarTurmaId(turmaId);
        TurmaId = turmaId;
        SelecionarCor = cor;
    }

    private void ValidarTurmaId(Guid turmaId)
    {
        if (turmaId == Guid.Empty)
            throw new ArgumentException("Id da turma inválido", nameof(turmaId));
    }
}