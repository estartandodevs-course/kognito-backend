using System;
using EstartandoDevsCore.Messages;

namespace Kognito.Turmas.App.Commands;

public class AtualizarConteudoCommand : Command
{

    public Guid ConteudoId { get; set; }
    public string Titulo { get; set; }
    public string ConteudoDidatico { get; set; }

    public AtualizarConteudoCommand(Guid conteudoId, string titulo, string conteudoDidatico, Guid classId)
    {
        ValidarParametros(conteudoId, titulo);
        ConteudoId = conteudoId;
        Titulo = titulo;
        ConteudoDidatico = conteudoDidatico;
    }
    private void ValidarParametros(Guid id, string titulo)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id do conteúdo inválido", nameof(id));
            
        if (string.IsNullOrWhiteSpace(titulo))
            throw new ArgumentException("Título não pode ser vazio", nameof(titulo));
    }
}
