using System;
using EstartandoDevsCore.Messages;
using Kognito.Turmas.Domain;


namespace Kognito.Turmas.App.Commands;

public class CriarTurmaCommand : Command
{
    public Guid Id { get;  set; }
    public Usuario Professor{ get;  set; }
    public string Nome { get;  set; }
    public string Descricao { get;  set; }
    public string Materia { get;  set; }
    public string LinkAcesso { get;  set; }

    public CriarTurmaCommand(Guid id, Usuario professor, string nome, string descricao, string materia, string linkAcesso)
    {
        Id = id;
        Professor = professor;
        Nome = nome;
        Descricao = descricao;
        Materia = materia;
        LinkAcesso = linkAcesso;
    }


}
