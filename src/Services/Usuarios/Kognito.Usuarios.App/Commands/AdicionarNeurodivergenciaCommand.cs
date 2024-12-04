using EstartandoDevsCore.Messages;

namespace Kognito.Usuarios.App.Commands;

public class AdicionarNeurodivergenciaCommand : Command
{
    public Guid UsuarioId { get; private set; }
    public Guid CodigoPai { get; private set; }
    public string Neurodivergencia { get; private set; }

    public AdicionarNeurodivergenciaCommand(Guid usuarioId, Guid codigoPai, string neurodivergencia)
    {
        UsuarioId = usuarioId;
        CodigoPai = codigoPai;
        Neurodivergencia = neurodivergencia;
    }
}