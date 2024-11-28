using System;
using EstartandoDevsCore.Messages;
using Kognito.Turmas.Domain;
using static Kognito.Turmas.Domain.Enturmamento;

namespace Kognito.Turmas.App.Commands;

public class CriarAlunoNaTurmaCommand : Command
{
    public Guid Id { get; set; }
    public Guid AlunoId { get; set; }
    public Guid TurmaId { get; set; }
    public EnturtamentoStatus Status { get; set; }

    public CriarAlunoNaTurmaCommand(Guid id, Guid alunoId, Guid turmaId, EnturtamentoStatus status)
    {
        Id = id;
        AlunoId = alunoId;
        TurmaId = turmaId;
        Status = status;
    }
    
}
