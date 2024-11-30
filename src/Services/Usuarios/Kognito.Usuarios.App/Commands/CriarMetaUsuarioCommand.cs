using System;
using EstartandoDevsCore.Messages;

namespace Kognito.Usuarios.App.Commands;

public class CriarMetaCommand : Command
{
    public string Titulo { get; set; }
    public string Descricao { get; set; }

    public CriarMetaCommand(string titulo, string descricao)
    {
        Titulo = titulo;
        Descricao = descricao;
    }
}
