using EstartandoDevsCore.Messages;

namespace Kognito.Usuario.App.Commands;

 public class RemoverEmblemaCommand : Command
    {
        public Guid EmblemaId { get; private set; }

        public RemoverEmblemaCommand(Guid emblemaId)
        {
            EmblemaId = emblemaId;
        }

    }

    
