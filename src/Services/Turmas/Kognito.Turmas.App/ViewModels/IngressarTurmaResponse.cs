using System;

namespace Kognito.Turmas.App.ViewModels;

public class IngressarTurmaResponse
{
    public bool Sucesso { get; set; }
    public string Mensagem { get; set; }
    public Guid TurmaId { get; set; }
    public string NomeTurma { get; set; }
    public string NomeProfessor { get; set; }
    public string Materia { get; set; }
    public DateTime DataIngresso { get; set; }

    public IngressarTurmaResponse()
    {
        DataIngresso = DateTime.UtcNow;
    }

    public IngressarTurmaResponse(bool sucesso, string mensagem, Guid turmaId, string nomeTurma, 
        string nomeProfessor, string materia)
    {
        Sucesso = sucesso;
        Mensagem = mensagem;
        TurmaId = turmaId;
        NomeTurma = nomeTurma;
        NomeProfessor = nomeProfessor;
        Materia = materia;
        DataIngresso = DateTime.UtcNow;
    }

    public static IngressarTurmaResponse Erro(string mensagem)
    {
        return new IngressarTurmaResponse
        {
            Sucesso = false,
            Mensagem = mensagem
        };
    }

    public static IngressarTurmaResponse CriarSucesso(Guid turmaId, string nomeTurma, 
        string nomeProfessor, string materia)
    {
        return new IngressarTurmaResponse
        {
            Sucesso = true,
            Mensagem = "Ingresso realizado com sucesso",
            TurmaId = turmaId,
            NomeTurma = nomeTurma,
            NomeProfessor = nomeProfessor,
            Materia = materia
        };
    }
}