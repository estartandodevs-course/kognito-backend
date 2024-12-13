using EstartandoDevsCore.Messages;

namespace Kognito.Usuarios.App.Commands;

public class RedefinirSenhaCommand : Command
{
    public string Email { get; private set; }
    public string CodigoRecuperacao { get; private set; }
    public string NovaSenha { get; private set; }

    public RedefinirSenhaCommand(string email, string codigoRecuperacao, string novaSenha)
    {
        Email = email;
        CodigoRecuperacao = codigoRecuperacao;
        NovaSenha = novaSenha;
    }
}