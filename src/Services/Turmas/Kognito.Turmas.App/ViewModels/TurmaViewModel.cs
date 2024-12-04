using System;
using Kognito.Turmas.Domain;

namespace Kognito.Turmas.App.ViewModels;

public class TurmaViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Subject { get; set; }
    public Cor Color { get; set; }
    public Icones Icons { get; set; }
    public DateTime RegistrationDate { get; set; }
    public DateTime? LastModifiedDate { get; set; }

    public string AccessHash { get; set; }
    public string AccessLink { get; set; }
    public UsuarioViewModel Professor { get; set; }

    public static TurmaViewModel Mapear(Turma turma)
    {
        if(turma == null)
            return null;

        return new TurmaViewModel
        {
            Id = turma.Id,
            Name = turma.Nome,
            Description = turma.Descricao,
            Subject = turma.Materia,
            Color = turma.Cor,
            Icons = turma.Icones,
            RegistrationDate = turma.DataDeCadastro,
            LastModifiedDate = turma.DataDeAlteracao,
            AccessHash = turma.HashAcesso,
            AccessLink = turma.LinkAcesso,
            Professor = new UsuarioViewModel 
            { 
                Id = turma.Professor.Id,
                Nome = turma.Professor.Nome
            }
        };
    }
}


