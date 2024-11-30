using EstartandoDevsCore.DomainObjects;

namespace Kognito.Turmas.Domain;

public class Enturmamento : Entity, IAggregateRoot
{

    public Usuario Aluno { get; set; }
    public Turma Turma { get; set; }
    public EnturtamentoStatus Status { get; set; }
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
    public void AtribuirAluno(Usuario aluno) => Aluno = aluno ?? throw new ArgumentNullException(nameof(aluno));

    public void AtrbuirTurma(Turma turma) => Turma = turma ?? throw new ArgumentNullException(nameof(turma));

    public enum EnturtamentoStatus
    {
        Ativo,
        Inativo,
        Suspenso
    }
    
}
