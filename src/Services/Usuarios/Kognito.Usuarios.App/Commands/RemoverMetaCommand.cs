using System;

using EstartandoDevsCore.Messages;

namespace Kognito.Usuarios.App.Commands;

    public class RemoverMetaCommand : Command
    {
        public Guid MetaId { get; private set; }

        public RemoverMetaCommand(Guid metaId)
        {
            MetaId = metaId;
        }
    }
   