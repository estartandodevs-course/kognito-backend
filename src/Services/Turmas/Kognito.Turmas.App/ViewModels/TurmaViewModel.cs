using System;
using System.Collections.Generic;
using System.Linq;
using Kognito.Turmas.Domain;

namespace Kognito.Turmas.App.ViewModels;

public class TurmaViewModel
{
<<<<<<< HEAD
    public Guid Id { get; set; }
    public Usuario Professor { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public string Materia { get; set; }
    public string LinkAcesso { get; set; }
    
    
    public Cor Cor { get; set; }
    public Icones Icones { get; set; }
=======
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public string Materia { get; private set; }
    public Cor Cor { get; private set; }
    public Icones Icones { get; private set; }
    public DateTime DataDeCadastro { get; private set; }
    public DateTime? DataDeAlteracao { get; private set; }
    public ICollection<LinkTurmaViewModel> LinksDeAcesso { get; private set; }

    public TurmaViewModel()
    {
        LinksDeAcesso = new List<LinkTurmaViewModel>();
    }

    public TurmaViewModel(Guid id, string nome, string descricao, string materia, 
        Cor cor, Icones icones, DateTime dataDeCadastro, DateTime? dataDeAlteracao)
    {
        Id = id;
        Nome = nome;
        Descricao = descricao;
        Materia = materia;
        Cor = cor;
        Icones = icones;
        DataDeCadastro = dataDeCadastro;
        DataDeAlteracao = dataDeAlteracao;
        LinksDeAcesso = new List<LinkTurmaViewModel>();
    }

>>>>>>> 892a573 (feat(turmas): implementa sistema de links de acesso para turmas)
    public static TurmaViewModel Mapear(Turma turma)
    {
        return new TurmaViewModel(
            turma.Id,
            turma.Nome,
            turma.Descricao,
            turma.Materia,
            turma.Cor,
            turma.Icones,
            turma.DataDeCadastro,
            turma.DataDeAlteracao)
        {
<<<<<<< HEAD
            Id = turma.Id,
            Professor = turma.Professor,
            Nome = turma.Nome,
            Descricao = turma.Descricao,
            Materia = turma.Materia,
            LinkAcesso = turma.LinkAcesso,
            Cor = turma.Cor,
            Icones = turma.Icones,
=======
            LinksDeAcesso = turma.LinksDeAcesso?.ToList()
                ?.Select(l => LinkTurmaViewModel.FromDomain(l, turma.Id))
                .ToList() ?? new List<LinkTurmaViewModel>()
>>>>>>> 892a573 (feat(turmas): implementa sistema de links de acesso para turmas)
        };
    }
}