using System;

namespace Kognito.Turmas.App.ViewModels;

public class LinkTurmaViewModel
{
    public string Link { get; set; }
    public string Codigo { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime DataExpiracao { get; set; }
    public bool Ativo { get; set; }
    public int? LimiteUsos { get; set; }
    public int QuantidadeUsos { get; set; }
    public TimeSpan TempoRestante { get; set; }
    public int? UsosRestantes { get; set; }

    public LinkTurmaViewModel()
    {
    }

    public LinkTurmaViewModel(string link, string codigo, DateTime dataCriacao, 
        DateTime dataExpiracao, bool ativo, int? limiteUsos, int quantidadeUsos)
    {
        Link = link;
        Codigo = codigo;
        DataCriacao = dataCriacao;
        DataExpiracao = dataExpiracao;
        Ativo = ativo;
        LimiteUsos = limiteUsos;
        QuantidadeUsos = quantidadeUsos;
        TempoRestante = dataExpiracao - DateTime.UtcNow;
        UsosRestantes = limiteUsos.HasValue ? limiteUsos - quantidadeUsos : null;
    }

    public static LinkTurmaViewModel FromDomain(LinkDeAcesso link, Guid turmaId)
    {
        return new LinkTurmaViewModel(
            link: link.ObterLinkCompleto(turmaId),
            codigo: link.Codigo,
            dataCriacao: link.DataCriacao,
            dataExpiracao: link.DataExpiracao,
            ativo: link.Ativo,
            limiteUsos: link.LimiteUsos > 0 ? link.LimiteUsos : null,
            quantidadeUsos: link.QuantidadeUsos
        );
    }

    public bool PodeSerUtilizado()
    {
        if (!Ativo) return false;
        if (DateTime.UtcNow > DataExpiracao) return false;
        if (LimiteUsos.HasValue && QuantidadeUsos >= LimiteUsos) return false;
        
        return true;
    }
}