using System;
using Kognito.Turmas.Domain;

namespace Kognito.Turmas.App.ViewModels;

public class TurmaViewModel
{
    public Guid Id { get; set; }
    public Usuario Professor { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public string Materia { get; set; }
    public string LinkAcesso { get; set; }
    
    
    public EnumParaCores Cor { get; set; }
    public EnumParaIcones Icones { get; set; }
    public static TurmaViewModel Mapear(Turma turma)
    {
        if(turma == null)
            return null;

        return new TurmaViewModel
        {
            Id = turma.Id,
            Professor = turma.Professor,
            Nome = turma.Nome,
            Descricao = turma.Descricao,
            Materia = turma.Materia,
            LinkAcesso = turma.LinkAcesso,
            Cor = turma.Cor,
            Icones = turma.Icones,
        };
    }
}


