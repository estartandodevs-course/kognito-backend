using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kognito.Turmas.Domain;
public class Conteudo
{
    public string Titulo { get; set; }
    public string ConteudoDidatico { get; set; }

    public Conteudo(string titulo, string conteudoDidatico)
    {
        Titulo = titulo;
        ConteudoDidatico = conteudoDidatico;
    }
}