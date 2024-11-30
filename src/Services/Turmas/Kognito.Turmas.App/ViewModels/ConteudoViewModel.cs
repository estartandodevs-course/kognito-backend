using System;
using Kognito.Turmas.Domain;

namespace Kognito.Turmas.App.ViewModels;

public class ConteudoViewModel
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public string ConteudoDidatico { get; set; }

    public static ConteudoViewModel Mapear(Conteudo conteudo)
    {
        if(conteudo == null)
            return null;

        return new ConteudoViewModel
        {
            Id = conteudo.Id,
            Titulo = conteudo.Titulo,
            ConteudoDidatico = conteudo.ConteudoDidatico
        };
    }
}
