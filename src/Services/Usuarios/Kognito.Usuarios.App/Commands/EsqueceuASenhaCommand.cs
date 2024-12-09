using EstartandoDevsCore.Messages;

namespace Kognito.Usuarios.App.Commands;

public class EsqueceuSenhaCommand : Command
{
    public string Email { get; private set; }

    public EsqueceuSenhaCommand(string email)
    {
        Email = email;
    }
}