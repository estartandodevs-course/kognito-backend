using System;

namespace Kognito.Turmas.App.ViewModels;

public class ProfessorViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; }

    public ProfessorViewModel(Guid id, string nome)
    {
        Id = id;
        Nome = nome;
    }

}
