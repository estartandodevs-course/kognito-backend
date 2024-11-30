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
        ValidarIds(turmaId, professorId);

        TurmaId = turmaId;
        ProfessorId = professorId;
    }
    private void ValidarIds(Guid turmaId, Guid professorId)
    {
        if (turmaId == Guid.Empty)
            throw new ArgumentException("Id da turma inválido", nameof(turmaId));
            
        if (professorId == Guid.Empty)
            throw new ArgumentException("Id do professor inválido", nameof(professorId));
    }
}
