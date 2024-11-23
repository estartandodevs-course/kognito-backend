using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kognito.Turmas.Domain;

public class Turma
{
    //Tirar esse string e colocar Usuario
    public string Professor{ get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public string Materia { get; set; }
    public string LinkAcesso { get; set; }

    public Turma(string professor, string nome, string descricao, string materia, string linkAcesso)
    {
        Professor = professor;
        Nome = nome;
        Descricao = descricao;
        Materia = materia;
        LinkAcesso = linkAcesso;
    }

}