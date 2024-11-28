using System;
using EstartandoDevsCore.Messages;

namespace Kognito.Turmas.App.Commands;

public class ExcluirConteudo : Command 
{

    public Guid ConteudoId { get; set; }

    public ExcluirConteudo(Guid conteudoId)
    {
        ConteudoId = conteudoId;

    }
}

