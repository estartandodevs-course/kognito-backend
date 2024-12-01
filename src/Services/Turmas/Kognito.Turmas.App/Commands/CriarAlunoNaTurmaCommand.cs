using EstartandoDevsCore.Messages;
using static Enturmamento;

namespace Kognito.Turmas.App.Commands;

public class CriarAlunoNaTurmaCommand : Command
{
    public Guid Id { get; private set; }
    public Guid AlunoId { get; private set; }
    public Guid TurmaId { get; private set; }
    public EnturtamentoStatus Status { get; private set; }

    public CriarAlunoNaTurmaCommand(Guid id, Guid alunoId, Guid turmaId, EnturtamentoStatus status)
    {
        ValidarIds(id, alunoId, turmaId);
        Id = id;
        AlunoId = alunoId;
        TurmaId = turmaId;
        Status = status;
    }
    
    private void ValidarIds(Guid id, Guid alunoId, Guid turmaId)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id inválido", nameof(id));
            
        if (alunoId == Guid.Empty)
            throw new ArgumentException("Id do aluno inválido", nameof(alunoId));
            
        if (turmaId == Guid.Empty)
            throw new ArgumentException("Id da turma inválido", nameof(turmaId));
    }
    
}
