using System;
using EstartandoDevsCore.Messages;

namespace Kognito.Turmas.App.Commands;

public class ExcluirConteudo : Command 
{

    public Guid ConteudoId { get; set; }

    public ExcluirConteudo(Guid conteudoId)
    {
        if (conteudoId == Guid.Empty)
            throw new ArgumentException("Id do conteúdo inválido", nameof(conteudoId));
            
        ConteudoId = conteudoId;

    }
}

