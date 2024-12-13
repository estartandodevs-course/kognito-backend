using System;
using EstartandoDevsCore.Messages;
using Kognito.Turmas.App.ViewModels;
using Kognito.Turmas.Domain;
using Kognito.Turmas.Domain.Common;
using FluentValidation.Results;

namespace Kognito.Turmas.App.Commands;

public class AtualizarTurmaCommand : Command
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public string Materia { get; private set; }

    public AtualizarTurmaCommand(Guid id, string nome, string descricao, string materia)
    {
        var validationResult = ValidarParametros(id, nome, materia);
        if (validationResult.Success)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            Materia = materia;
        }
        else
        {
            foreach (var error in validationResult.Errors)
                ValidationResult.Errors.Add(new ValidationFailure(string.Empty, error));
        }
    }

    private Result ValidarParametros(Guid id, string nome, string materia)
    {
        var errors = new List<string>();

        if (id == Guid.Empty)
            errors.Add("Id da turma inválido");
            
        if (string.IsNullOrWhiteSpace(nome))
            errors.Add("Nome não pode ser vazio");
            
        if (string.IsNullOrWhiteSpace(materia))
            errors.Add("Matéria não pode ser vazia");

        return errors.Any() ? Result.Fail(errors) : Result.Ok();
    }

    public override bool EstaValido()
    {
        return ValidationResult.IsValid;
    }
}