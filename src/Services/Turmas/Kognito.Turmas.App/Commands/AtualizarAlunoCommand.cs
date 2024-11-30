using System;
using EstartandoDevsCore.Messages;
using Kognito.Turmas.Domain;

namespace Kognito.Turmas.App.Commands;

public class AtualizarAlunoCommand : Command
{

    public Guid Id { get; set; }
    public Guid AlunoId { get; set; }


    public AtualizarAlunoCommand(Guid id, Guid alunoId)
    {
        ValidarIds(id, alunoId);
        Id = id;
        AlunoId = alunoId;
    }
    private void ValidarIds(Guid id, Guid alunoId)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id inválido", nameof(id));
            
        if (alunoId == Guid.Empty)
            throw new ArgumentException("Id do aluno inválido", nameof(alunoId));
    }
 
}
