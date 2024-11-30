using EstartandoDevsCore.DomainObjects;
using Kognito.Turmas.Domain;

public class Enturmamento : Entity, IAggregateRoot
{
    public Usuario Aluno { get; private set; }
    public Turma Turma { get; private set; }
    public EnturtamentoStatus Status { get; private set; }
    
    protected Enturmamento(){}
    
    public Enturmamento(Usuario aluno, Turma turma, EnturtamentoStatus status)
    {
        ValidarCampos(aluno, turma);
        Aluno = aluno;
        Turma = turma;
        Status = status;
    }

    private void ValidarCampos(Usuario aluno, Turma turma)
    {
        if (aluno == null) throw new ArgumentNullException(nameof(aluno));
        if (turma == null) throw new ArgumentNullException(nameof(turma));
    }

    public void AtribuirAluno(Usuario aluno) => Aluno = aluno ?? 
        throw new ArgumentNullException(nameof(aluno));

    public void AtribuirTurma(Turma turma) => Turma = turma ?? 
        throw new ArgumentNullException(nameof(turma));

    public void AtualizarStatus(EnturtamentoStatus novoStatus)
    {
        Status = novoStatus;
    }

    public enum EnturtamentoStatus
    {
        Ativo,
        Inativo,
        Suspenso
    }
}