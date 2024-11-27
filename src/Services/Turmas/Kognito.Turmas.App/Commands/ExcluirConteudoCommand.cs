using System;
using EstartandoDevsCore.Messages;

namespace Kognito.Turmas.App.Commands;

public class ExcluirConteudo : Command 
{

    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public string ConteudoDidatico { get; set; }

    public ExcluirConteudo(Guid id, string titulo, string conteudoDidatico)
    {
        Id = id;
        Titulo = titulo;
        ConteudoDidatico = conteudoDidatico;
    }
}

