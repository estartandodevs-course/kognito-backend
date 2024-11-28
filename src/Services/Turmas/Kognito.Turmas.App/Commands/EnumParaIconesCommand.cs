using System;
using static Kognito.Turmas.App.ViewModels.EnumParaIconesViewModel;

namespace Kognito.Turmas.App.Commands;

public class EnumParaIconesCommand
{

    public Icones  SelecionarIcones { get; set; }

    public EnumParaIconesCommand(Icones selecionarIcones)
    {
        SelecionarIcones = selecionarIcones;
    }
}
