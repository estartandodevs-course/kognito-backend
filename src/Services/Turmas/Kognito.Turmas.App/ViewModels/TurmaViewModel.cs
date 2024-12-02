using System;
using Kognito.Turmas.Domain;

namespace Kognito.Turmas.App.ViewModels;

public class TurmaViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public string Materia { get; set; }
    public Cor Cor { get; set; }
    public Icones Icones { get; set; }
    public DateTime DataDeCadastro { get; set; }
    public DateTime? DataDeAlteracao { get; set; }


    public static TurmaViewModel Mapear(Turma turma)
    {
        if(turma == null)
            return null;

        return new TurmaViewModel
        {
            Id = turma.Id,
            Nome = turma.Nome,
            Descricao = turma.Descricao,
            Materia = turma.Materia,
            Cor = turma.Cor,
            Icones = turma.Icones,
            DataDeCadastro = turma.DataDeCadastro,
            DataDeAlteracao = turma.DataDeAlteracao,

        };
    }
}


