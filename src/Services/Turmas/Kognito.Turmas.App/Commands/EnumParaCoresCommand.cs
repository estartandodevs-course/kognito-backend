using System;
using static Kognito.Turmas.App.ViewModels.EnumParaCoresViewModel;

namespace Kognito.Turmas.App.Commands;

public class EnumParaCoresCommand
{
    public Cor SelecionarCor{ get; set; }

    public EnumParaCoresCommand(Cor selecionarCor)
    {
        SelecionarCor = selecionarCor;
    }
}
