using System;
using EstartandoDevsCore.Messages;
using Kognito.Turmas.Domain.Common;
using FluentValidation.Results;

namespace Kognito.Turmas.App.Commands;

public class ExcluirConteudoCommand : Command 
{
    public Guid ConteudoId { get; private set; }

    public ExcluirConteudoCommand(Guid conteudoId)
    {
        var validationResult = ValidarId(conteudoId);
        if (validationResult.Success)
        {
            ConteudoId = conteudoId;
        }
        else
        {
            foreach (var error in validationResult.Errors)
                ValidationResult.Errors.Add(new ValidationFailure(string.Empty, error));
        }
    }

    private Result ValidarId(Guid conteudoId)
    {
        if (conteudoId == Guid.Empty)
            return Result.Fail("Id do conteúdo inválido");

        return Result.Ok();
    }

    public override bool EstaValido()
    {
        return ValidationResult.IsValid;
    }
}