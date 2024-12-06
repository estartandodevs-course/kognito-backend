using EstartandoDevsCore.DomainObjects;
using Kognito.Turmas.Domain;
using Kognito.Turmas.Domain.Common;

public class Enturmamento : Entity, IAggregateRoot
{
    public Usuario Aluno { get; private set; }
    public Turma Turma { get; private set; }
    public EnturtamentoStatus Status { get; private set; }
    
    protected Enturmamento() { }

    public static Result<Enturmamento> Criar(Usuario aluno, Turma turma, EnturtamentoStatus status)
    {
        if (aluno == null)
            return Result<Enturmamento>.Fail("Aluno não pode ser nulo");

        if (turma == null)
            return Result<Enturmamento>.Fail("Turma não pode ser nula");

        var enturmamento = new Enturmamento
        {
            Aluno = aluno,
            Turma = turma,
            Status = status
        };

        return Result<Enturmamento>.Ok(enturmamento, "Enturmamento criado com sucesso");
    }

    public Result AtribuirAluno(Usuario aluno)
    {
        if (aluno == null)
            return Result.Fail("Aluno não pode ser nulo");

        Aluno = aluno;
        return Result.Ok("Aluno atribuído com sucesso");
    }

    public Result AtribuirTurma(Turma turma)
    {
        if (turma == null)
            return Result.Fail("Turma não pode ser nula");

        Turma = turma;
        return Result.Ok("Turma atribuída com sucesso");
    }

    public Result AtualizarStatus(EnturtamentoStatus novoStatus)
    {
        Status = novoStatus;
        return Result.Ok($"Status atualizado para {novoStatus}");
    }

    public enum EnturtamentoStatus
    {
        Ativo,
        Inativo,
        Suspenso
    }
}