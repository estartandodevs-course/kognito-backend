using System;
using EstartandoDevsCore.Messages;

namespace Kognito.Turmas.App.Commands;

public class ExcluirConteudoCommand : Command 
{

    public Guid ConteudoId { get; set; }

    public ExcluirConteudoCommand(Guid conteudoId)
    {
        if (conteudoId == Guid.Empty)
            throw new ArgumentException("Id do conteúdo inválido", nameof(conteudoId));
            
        ConteudoId = conteudoId;

    }
}

