using EstartandoDevsCore.Messages;
using System;

namespace Kognito.Usuario.App.Commands;
public class RemoverUsuarioCommand : Command
{
    public Guid UsuarioId { get; private set; }
    public RemoverUsuarioCommand(Guid usuarioId)
    {
         UsuarioId = usuarioId;
    }

}

