using System;
using EstartandoDevsCore.Messages;
using EstartandoDevsCore.ValueObjects;
using Kognito.Turmas.Domain;

namespace Kognito.Turmas.App.Commands;

public class AtribuirProfessorCommand : Command
{
    public Guid TurmaId { get; set; }
    public Guid ProfessorId { get; set; }
    
    public AtribuirProfessorCommand(Guid turmaId, Guid professorId)
    {
        TurmaId = turmaId;
        ProfessorId = professorId;
    }
}
