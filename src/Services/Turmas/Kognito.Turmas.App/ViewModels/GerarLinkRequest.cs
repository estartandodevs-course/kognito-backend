using System;

namespace Kognito.Turmas.App.ViewModels;

public class GerarLinkRequest
{
    public int? LimiteUsos { get; set; }
    public int? DiasValidade { get; set; }

    public GerarLinkRequest()
    {
        LimiteUsos = 0;
        DiasValidade = 7;
    }

    public GerarLinkRequest(int? limiteUsos, int? diasValidade)
    {
        LimiteUsos = limiteUsos;
        DiasValidade = diasValidade;
    }
}