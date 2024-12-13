using System;
using EstartandoDevsCore.Messages;
using Kognito.Turmas.Domain;
using Kognito.Turmas.Domain.Common;
using FluentValidation.Results;

namespace Kognito.Turmas.App.Commands;

public class ExcluirTurmaCommand : Command
{
    public Guid TurmaId { get; private set; }

    public ExcluirTurmaCommand(Guid turmaId)
    {
        var validationResult = ValidarId(turmaId);
        if (validationResult.Success)
        {
            TurmaId = turmaId;
        }
        else
        {
            foreach (var error in validationResult.Errors)
                ValidationResult.Errors.Add(new ValidationFailure(string.Empty, error));
        }
    }

    private Result ValidarId(Guid turmaId)
    {
        if (turmaId == Guid.Empty)
            return Result.Fail("Id da turma inválido");

        return Result.Ok();
    }

    public override bool EstaValido()
    {
        return ValidationResult.IsValid;
    }
}