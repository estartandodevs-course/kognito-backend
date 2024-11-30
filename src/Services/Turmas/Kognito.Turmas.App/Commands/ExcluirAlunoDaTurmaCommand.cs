using System;
using EstartandoDevsCore.Messages;
using Kognito.Turmas.Domain;
using static Kognito.Turmas.Domain.Enturmamento;

namespace Kognito.Turmas.App.Commands;

public class ExcluirAlunoDaTurmaCommand : Command
{
    public Guid AlunoId { get; set; }
    public Guid TurmaId { get; set; }
    public EnturtamentoStatus Status { get; set; }
    public ExcluirAlunoDaTurmaCommand(Guid alunoId, Guid turmaId, EnturtamentoStatus status)
    {
        ValidarIds(alunoId, turmaId);
        AlunoId = alunoId;
        TurmaId = turmaId;
        Status = status;
    }
     private void ValidarIds(Guid alunoId, Guid turmaId)
    {
        if (alunoId == Guid.Empty)
            throw new ArgumentException("Id do aluno inválido", nameof(alunoId));
            
        if (turmaId == Guid.Empty)
            throw new ArgumentException("Id da turma inválido", nameof(turmaId));
    }
}
