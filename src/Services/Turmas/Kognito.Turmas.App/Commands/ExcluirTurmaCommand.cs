using System;
using EstartandoDevsCore.Messages;
using Kognito.Turmas.Domain;

namespace Kognito.Turmas.App.Commands;

public class ExcluirTurmaCommand : Command
{
    public Guid Id { get; private set; }
    public Usuario Professor{ get; private set; }
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public string Materia { get; private set; }
    public string LinkAcesso { get; private set; }

    public ExcluirTurmaCommand(Guid id, Usuario professor, string nome, string descricao, string materia, string linkAcesso)
    {
        Id = id;
        Professor = professor;
        Nome = nome;
        Descricao = descricao;
        Materia = materia;
        LinkAcesso = linkAcesso;
    }

    
}
