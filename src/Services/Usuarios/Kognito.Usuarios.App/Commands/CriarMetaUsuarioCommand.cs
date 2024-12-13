using EstartandoDevsCore.Messages;
using FluentValidation;

namespace Kognito.Usuarios.App.Commands;

public class CriarMetaCommand : Command
{
    public Guid UsuarioId { get; private set; }
    public string Titulo { get; private set; }
    public string Descricao { get; private set; }

    public CriarMetaCommand(Guid usuarioId, string titulo, string descricao)
    {
        UsuarioId = usuarioId;
        Titulo = titulo;
        Descricao = descricao;
    }
    
}