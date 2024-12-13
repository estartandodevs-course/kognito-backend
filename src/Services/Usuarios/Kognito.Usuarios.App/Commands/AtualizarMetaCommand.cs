using System;
using EstartandoDevsCore.Messages;

namespace Kognito.Usuarios.App.Commands;

public class AtualizarMetaCommand : Command 
{
    public Guid MetaId { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }

    public AtualizarMetaCommand(Guid metaId, string titulo, string descricao)
    {
        MetaId = metaId;
        Titulo = titulo;
        Descricao = descricao;
    }
}
