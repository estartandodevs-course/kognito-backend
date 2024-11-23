using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kognito.Turmas.Domain;

public class Enturmamento
{
    // trocar string por usuario
    public string Aluno { get; set; }
    public Turma Turma { get; set; }
    public string Status { get; set; }

    public Enturmamento(string aluno, Turma turma, string status)
    {
        Aluno = aluno;
        Turma = turma;
        Status = status;
    }
    
}
