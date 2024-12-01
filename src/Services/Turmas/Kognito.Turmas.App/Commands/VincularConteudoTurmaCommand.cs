using EstartandoDevsCore.Messages;

public class VincularConteudoTurmaCommand : Command
{
    public Guid ConteudoId { get; private set; }
    public Guid TurmaId { get; private set; }

    public VincularConteudoTurmaCommand(Guid conteudoId, Guid turmaId)
    {
        ValidarIds(conteudoId, turmaId);
        ConteudoId = conteudoId;
        TurmaId = turmaId;
    }

    private void ValidarIds(Guid conteudoId, Guid turmaId)
    {
        if (conteudoId == Guid.Empty)
            throw new ArgumentException("Id do conteúdo inválido", nameof(conteudoId));
            
        if (turmaId == Guid.Empty)
            throw new ArgumentException("Id da turma inválido", nameof(turmaId));
    }
}
