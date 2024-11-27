using System;
using EstartandoDevsCore.Messages;
using Kognito.Turmas.Domain;
using static Kognito.Turmas.Domain.Enturmamento;

namespace Kognito.Turmas.App.Commands;

public class CriarAlunoNaTurmaCommand : Command
{

    public Guid Id { get; set; }
    public Usuario Aluno { get; set; }
    public Turma Turma { get; set; }
    public EnturtamentoStatus Status { get; set; }

    public CriarAlunoNaTurmaCommand(Guid id, Usuario aluno, Turma turma, EnturtamentoStatus status)
    {
        Id = id;
        Aluno = aluno;
        Turma = turma;
        Status = status;
    }
    
}
