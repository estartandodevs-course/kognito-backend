using EstartandoDevsCore.Messages;

namespace Kognito.Usuario.App.Commands;

public class AtualizarUsuarioCommand : Command
{
    public Guid UsuarioId { get; set; }
    public string Nome { get; set; }
    public string Neurodivergencia { get; set; }

    public AtualizarUsuarioCommand(Guid usuarioId, string nome, string neurodivergencia)
    {
        UsuarioId = usuarioId;
        Nome = nome;
        Neurodivergencia = neurodivergencia;
    }
}