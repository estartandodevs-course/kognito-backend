using EstartandoDevsCore.Messages;
using Kognito.Turmas.Domain.Common;
using FluentValidation.Results;
using static Enturmamento;

namespace Kognito.Turmas.App.Commands;

public class CriarAlunoNaTurmaCommand : Command
{
    public Guid Id { get; private set; }
    public Guid AlunoId { get; private set; }
    public string AlunoNome { get; private set; }
    public Guid TurmaId { get; private set; }
    public EnturtamentoStatus Status { get; private set; }

  private Result ValidarIds(Guid id, Guid alunoId, Guid turmaId, string alunoNome)
{
    var errors = new List<string>();

    if (id == Guid.Empty)
        errors.Add("Id inválido");
        
    if (alunoId == Guid.Empty)
        errors.Add("Id do aluno inválido");
        
    if (turmaId == Guid.Empty)
        errors.Add("Id da turma inválido");

    if (string.IsNullOrWhiteSpace(alunoNome))
        errors.Add("Nome do aluno não pode ser vazio");

    return errors.Any() ? Result.Fail(errors) : Result.Ok();
}

public CriarAlunoNaTurmaCommand(
    Guid id,
    Guid alunoId,
    string alunoNome,
    Guid turmaId,
    EnturtamentoStatus status)
{
    var validationResult = ValidarIds(id, alunoId, turmaId, alunoNome);
    if (validationResult.Success)
    {
        Id = id;
        AlunoId = alunoId;
        AlunoNome = alunoNome;
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