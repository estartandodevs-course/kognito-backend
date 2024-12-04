using System;

namespace Kognito.Turmas.App.ViewModels;

public class TurmaCriadaViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string HashAcesso { get; set; }
    public string LinkAcesso { get; set; }
    public Guid ProfessorId { get; set; }

    public static TurmaCriadaViewModel Mapear(Turma turma)
    {
        return new TurmaCriadaViewModel
        {
            Id = turma.Id,
            Nome = turma.Nome,
            HashAcesso = turma.HashAcesso,
            LinkAcesso = turma.LinkAcesso,
            ProfessorId = turma.Professor.Id
        };
    }
}