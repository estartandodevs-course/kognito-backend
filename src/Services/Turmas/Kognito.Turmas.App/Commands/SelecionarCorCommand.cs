using System;
using EstartandoDevsCore.Messages;
using static Kognito.Turmas.Domain.EnumParaCores;

namespace Kognito.Turmas.App.Commands;

public class SelecionarCorCommand : Command
{
    public Cor SelecionarCor{ get; set; }

    public SelecionarCorCommand(Cor selecionarCor)
    {
        SelecionarCor = selecionarCor;
    }
}
