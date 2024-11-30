using System;
using EstartandoDevsCore.Messages;
using static Kognito.Turmas.Domain.EnumParaIcones;


namespace Kognito.Turmas.App.Commands;

public class SelecionarIconesCommand : Command
{

    public Icones  SelecionarIcones { get; set; }

    public SelecionarIconesCommand(Icones selecionarIcones)
    {
        SelecionarIcones = selecionarIcones;
    }
}
