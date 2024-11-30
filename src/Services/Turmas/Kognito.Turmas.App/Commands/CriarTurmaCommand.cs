using System;
using EstartandoDevsCore.Messages;
using FluentValidation.Results;
using Kognito.Turmas.Domain;
using MediatR;

namespace Kognito.Turmas.App.Commands;

public class CriarTurmaCommand : Command, IRequest<ValidationResult>
{
    public Guid Id { get;  set; }
    public Usuario Professor{ get;  set; }
    public string Nome { get;  set; }
    public string Descricao { get;  set; }
    public string Materia { get;  set; }
    public string LinkAcesso { get;  set; }

    public CriarTurmaCommand(Guid id, Usuario professor, string nome, string descricao, string materia, string linkAcesso)
    {
        ValidarParametros(id, professor, nome, materia);
        Id = id;
        Professor = professor;
        Nome = nome;
        Descricao = descricao;
        Materia = materia;
        LinkAcesso = linkAcesso;
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
