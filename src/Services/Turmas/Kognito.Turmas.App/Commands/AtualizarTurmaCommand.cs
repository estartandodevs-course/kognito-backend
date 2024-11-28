using System;
using EstartandoDevsCore.Messages;
using Kognito.Turmas.App.ViewModels;
using Kognito.Turmas.Domain;

namespace Kognito.Turmas.App.Commands;

public class AtualizarTurmaCommand : Command
{

    public Guid Id { get; private set; }
    public Usuario Professor{ get; private set; }
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public string Materia { get; private set; }
    public string LinkAcesso { get; private set; }

    public AtualizarTurmaCommand(Guid id, Usuario professor, string nome, string descricao, string materia, string linkAcesso)
    {
        Id = id;
        Professor = professor;
        Nome = nome;
        Descricao = descricao;
        Materia = materia;
        LinkAcesso = linkAcesso;
    }
}
