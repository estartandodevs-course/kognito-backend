using System;
using Kognito.Turmas.Domain;
using static Kognito.Turmas.Domain.Enturmamento;

namespace Kognito.Turmas.App.Commands;

public class ExcluirAlunoDaTurmaCommand
{
    public Guid AlunoId { get; set; }
    public Guid TurmaId { get; set; }
    public EnturtamentoStatus Status { get; set; }
    public ExcluirAlunoDaTurmaCommand(Guid alunoId, Guid turmaId, EnturtamentoStatus status)
    {
        AlunoId = alunoId;
        TurmaId = turmaId;
        Status = status;
    }
}
