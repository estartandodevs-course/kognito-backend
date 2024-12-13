using EstartandoDevsCore.Messages;
using Kognito.Turmas.Domain.Common;
using FluentValidation.Results;

public class VincularConteudoTurmaCommand : Command
{
    public Guid ConteudoId { get; private set; }
    public Guid TurmaId { get; private set; }

    public VincularConteudoTurmaCommand(Guid conteudoId, Guid turmaId)
    {
        var validationResult = ValidarIds(conteudoId, turmaId);
        if (validationResult.Success)
        {
            ConteudoId = conteudoId;
            TurmaId = turmaId;
        }
        else
        {
            foreach (var error in validationResult.Errors)
                ValidationResult.Errors.Add(new ValidationFailure(string.Empty, error));
        }
    }

    private Result ValidarIds(Guid conteudoId, Guid turmaId)
    {
        var errors = new List<string>();

        if (conteudoId == Guid.Empty)
            errors.Add("Id do conteúdo inválido");
            
        if (turmaId == Guid.Empty)
            errors.Add("Id da turma inválido");

        return errors.Any() ? Result.Fail(errors) : Result.Ok();
    }

    public override bool EstaValido()
    {
        return ValidationResult.IsValid;
    }
}