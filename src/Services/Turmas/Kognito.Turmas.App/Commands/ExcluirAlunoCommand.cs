using System;

namespace Kognito.Turmas.App.Commands;

public class ExcluirAlunoCommand
{
    public Guid AlunoId { get; set; }
    
    public ExcluirAlunoCommand(Guid alunoId)
    {
        AlunoId = alunoId;
    }
}
