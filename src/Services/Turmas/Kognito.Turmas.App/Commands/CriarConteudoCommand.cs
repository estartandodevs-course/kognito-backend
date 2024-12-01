using System;
using EstartandoDevsCore.Messages;

namespace Kognito.Turmas.App.Commands;

public class CriarConteudoCommand : Command
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public string ConteudoDidatico { get; set; }
    public CriarConteudoCommand(Guid id, string titulo, string conteudoDidatico)
    {
        ValidarParametros(id, titulo);
        Id = id;
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
