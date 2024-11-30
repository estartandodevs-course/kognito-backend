using System;
using EstartandoDevsCore.Messages;
using Kognito.Turmas.Domain;

namespace Kognito.Turmas.App.Commands;

public class CriarAlunoCommand : Command
{
    public Guid Id { get; set; }
    public Usuario Aluno { get; set; }


    public CriarAlunoCommand(Guid id, Usuario aluno)
    {
        ValidarParametros(id, aluno);
        Id = id;
        Aluno = aluno;
    }
    private void ValidarParametros(Guid id, Usuario aluno)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id inválido", nameof(id));
            
        if (aluno == null)
            throw new ArgumentException("Aluno não pode ser nulo", nameof(aluno));
    }

}
