using EstartandoDevsCore.Messages;

namespace Kognito.Usuario.App.Commands;

public class ConcluirMetaCommand : Command
{
    public Guid MetaId { get; set; }

    public ConcluirMetaCommand(Guid metaId)
    {
        MetaId = metaId;
    }
}