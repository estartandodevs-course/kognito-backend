using EstartandoDevsCore.DomainObjects;

namespace Kognito.Turmas.Domain;

public class Enturmamento : Entity, IAggregateRoot
{
    // trocar string por usuario
    public string Aluno { get; set; }
    public Turma Turma { get; set; }
    public EnturtamentoStatus Status { get; set; }
    public Enturmamento(string aluno, Turma turma, EnturtamentoStatus status)
    {
        Aluno = aluno;
        Turma = turma;
        Status = status;
    }
    private Enturmamento(){}
    
    public void AtribuirAluno(string aluno) => Aluno = aluno;

    public void AtrbuirTurma(string turma) => Turma = Turma; 

    public enum EnturtamentoStatus
    {
        Ativo,
        Inativo,
        Supenso
    }
    
}
