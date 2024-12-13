using EstartandoDevsCore.Messages;
using Kognito.Turmas.Domain;
using Kognito.Turmas.Domain.Common;
using FluentValidation.Results;

public class SelecionarCorCommand : Command
{
    public Guid TurmaId { get; private set; }
    public Cor SelecionarCor { get; private set; }

    public SelecionarCorCommand(Guid turmaId, Cor cor)
    {
        var validationResult = ValidarTurmaId(turmaId);
        if (validationResult.Success)
        {
            TurmaId = turmaId;
            SelecionarCor = cor;
        }
        else
        {
            foreach (var error in validationResult.Errors)
                ValidationResult.Errors.Add(new ValidationFailure(string.Empty, error));
        }
    }

    private Result ValidarTurmaId(Guid turmaId)
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