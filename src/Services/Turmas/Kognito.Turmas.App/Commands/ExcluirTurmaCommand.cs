using System;
using EstartandoDevsCore.Messages;
using Kognito.Turmas.Domain;

namespace Kognito.Turmas.App.Commands;

public class ExcluirTurmaCommand : Command
{
    public Guid TurmaId { get; private set; }

    public ExcluirTurmaCommand(Guid turmaId)
    {
        TurmaId = turmaId;
    }

    
}
