using System;
using Kognito.Turmas.Domain;

namespace Kognito.Turmas.App.Commands;

public class CriarAlunoCommand
{
    public Guid Id { get; set; }
    public Usuario Aluno { get; set; }
    public string Neurodivergencia { get; set; }
    public int Ofensiva { get; set; }

    public CriarAlunoCommand(Guid id, Usuario aluno, string neurodivergencia, int ofensiva)
    {
        Id = id;
        Aluno = aluno;
        Neurodivergencia = neurodivergencia;
        Ofensiva = ofensiva;
    }

}