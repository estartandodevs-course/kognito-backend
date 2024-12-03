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


    public AtualizarTurmaCommand(Guid id, Usuario professor, string nome, string descricao, string materia)
    {
        ValidarParametros(id, professor, nome, materia);
        Id = id;
        Professor = professor;
        Nome = nome;
        Descricao = descricao;
        Materia = materia;
    }
      private void ValidarParametros(Guid id, Usuario professor, string nome, string materia)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id da turma inválido", nameof(id));
            
        if (professor == null)
            throw new ArgumentException("Professor não pode ser nulo", nameof(professor));
            
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome não pode ser vazio", nameof(nome));
            
        if (string.IsNullOrWhiteSpace(materia))
            throw new ArgumentException("Matéria não pode ser vazia", nameof(materia));
    }
}
