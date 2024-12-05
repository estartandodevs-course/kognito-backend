using System;
using EstartandoDevsCore.Messages;

namespace Kognito.Turmas.App.Commands;

public class CriarConteudoCommand : Command
{

    public Guid Id { get; private set; }
    public string Titulo { get; private set; }
    public string ConteudoDidatico { get; private set; }
    public Guid TurmaId { get; private set; }

    public CriarConteudoCommand(Guid id, string titulo, string conteudoDidatico, Guid turmaId)
    {
        Id = id;
        Titulo = titulo;
        ConteudoDidatico = conteudoDidatico;
        TurmaId = turmaId;
    }

    private void ValidarParametros(Guid id, string titulo)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id do conteúdo inválido", nameof(id));
            
        if (string.IsNullOrWhiteSpace(titulo))
            throw new ArgumentException("Título não pode ser vazio", nameof(titulo));
    }
}
