using System;
using EstartandoDevsCore.Messages;
using EstartandoDevsCore.ValueObjects;
using FluentValidation.Results;
using Kognito.Turmas.Domain;
using Kognito.Turmas.Domain.Common;

namespace Kognito.Turmas.App.Commands;

public class AtribuirProfessorCommand : Command
{
    public Guid TurmaId { get; private set; }
    public Guid ProfessorId { get; private set; }
    
    public AtribuirProfessorCommand(Guid turmaId, Guid professorId)
    {
        var validationResult = ValidarIds(turmaId, professorId);
        if (validationResult.Success)
        {
            TurmaId = turmaId;
            ProfessorId = professorId;
        }
        else
        {
            foreach (var error in validationResult.Errors)
                ValidationResult.Errors.Add(new ValidationFailure(string.Empty, error));
        }
    }

    private Result ValidarIds(Guid turmaId, Guid professorId)
    {
        var errors = new List<string>();

        if (turmaId == Guid.Empty)
            errors.Add("Id da turma inválido");
            
        if (professorId == Guid.Empty)
            errors.Add("Id do professor inválido");

        return errors.Any() ? Result.Fail(errors) : Result.Ok();
    }

    public override bool EstaValido()
    {
        return ValidationResult.IsValid;
    }
}