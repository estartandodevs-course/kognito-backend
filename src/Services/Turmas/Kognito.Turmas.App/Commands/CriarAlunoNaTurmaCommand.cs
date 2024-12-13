using EstartandoDevsCore.Messages;
using Kognito.Turmas.Domain.Common;
using FluentValidation.Results;
using static Enturmamento;

namespace Kognito.Turmas.App.Commands;

public class CriarAlunoNaTurmaCommand : Command
{
    public Guid AlunoId { get; private set; }
    public Guid TurmaId { get; private set; }
    public EnturtamentoStatus Status { get; private set; }

    private Result ValidarIds(Guid alunoId, Guid turmaId)
    {
        var errors = new List<string>();
        
        if (alunoId == Guid.Empty)
            errors.Add("Id do aluno inválido");
            
        if (turmaId == Guid.Empty)
            errors.Add("Id da turma inválido");

        return errors.Any() ? Result.Fail(errors) : Result.Ok();
    }

    public CriarAlunoNaTurmaCommand(
        Guid alunoId,
        Guid turmaId,
        EnturtamentoStatus status)
    {
        var validationResult = ValidarIds(alunoId, turmaId);
        if (validationResult.Success)
        {
            AlunoId = alunoId;
            TurmaId = turmaId;
            Status = status;
        }
        else
        {
            foreach (var error in validationResult.Errors)
                ValidationResult.Errors.Add(new ValidationFailure(string.Empty, error));
        }
    }

    public override bool EstaValido()
    {
        return ValidationResult.IsValid;
    }
}