using System;
using EstartandoDevsCore.Messages;

namespace Kognito.Turmas.App.Commands;

public class ExcluirAlunoCommand : Command
{
    public Guid AlunoId { get; set; }
    
    public ExcluirAlunoCommand(Guid alunoId)
    {
        if (alunoId == Guid.Empty)
            throw new ArgumentException("Id do aluno inválido", nameof(alunoId));
            
        AlunoId = alunoId;
    }
}
