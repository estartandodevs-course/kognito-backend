using System;
using EstartandoDevsCore.Messages;
using Kognito.Turmas.Domain.Common;
using FluentValidation.Results;

namespace Kognito.Turmas.App.Commands;

public class CriarConteudoCommand : Command
{
    public Guid Id { get; private set; }
    public string Titulo { get; private set; }
    public string ConteudoDidatico { get; private set; }
    public Guid TurmaId { get; private set; }

    public CriarConteudoCommand(Guid id, string titulo, string conteudoDidatico, Guid turmaId)
    {
        var validationResult = ValidarParametros(id, titulo, turmaId);
        if (validationResult.Success)
        {
            Id = id;
            Titulo = titulo;
            ConteudoDidatico = conteudoDidatico;
            TurmaId = turmaId;
        }
        else
        {
            foreach (var error in validationResult.Errors)
                ValidationResult.Errors.Add(new ValidationFailure(string.Empty, error));
        }
    }

    private Result ValidarParametros(Guid id, string titulo, Guid turmaId)
    {
        var errors = new List<string>();

        if (id == Guid.Empty)
            errors.Add("Id do conteúdo inválido");
            
        if (string.IsNullOrWhiteSpace(titulo))
            errors.Add("Título não pode ser vazio");

        if (turmaId == Guid.Empty)
            errors.Add("Id da turma inválido");

        return errors.Any() ? Result.Fail(errors) : Result.Ok();
    }

    public override bool EstaValido()
    {
        return ValidationResult.IsValid;
    }
}