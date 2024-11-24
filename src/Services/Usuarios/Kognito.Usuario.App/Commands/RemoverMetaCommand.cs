using EstartandoDevsCore.Messages;

namespace Kognito.Usuario.App.Commands;

    public class RemoverMetaCommand : Command
    {
        public Guid MetaId { get; private set; }

        public RemoverMetaCommand(Guid metaId)
        {
            MetaId = metaId;
        }
    }
   