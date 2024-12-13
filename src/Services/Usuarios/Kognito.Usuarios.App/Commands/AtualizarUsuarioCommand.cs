using System;

using EstartandoDevsCore.Messages;

namespace Kognito.Usuarios.App.Commands;

public class AtualizarUsuarioCommand : Command
{
    public Guid UsuarioId { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }

    public AtualizarUsuarioCommand(Guid usuarioId, string nome, string email)
    {
        UsuarioId = usuarioId;
        Nome = nome;
        Email = email;
    }
}