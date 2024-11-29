using EstartandoDevsCore.Messages;
using FluentValidation;

namespace Kognito.Tarefas.App.Commands;

public class AtualizarNotaCommand : Command
{
    public Guid Id { get; private set; }
    public double ValorNota { get; private set; }
    public Guid AlunoId { get; private set; }
    public Guid TurmaId { get; private set; }
    public Guid EntregaId { get; private set; }

    public AtualizarNotaCommand(Guid id, double valorNota, Guid alunoId, Guid turmaId, Guid entregaId)
    {
        Id = id;
        ValorNota = valorNota;
        AlunoId = alunoId;
        TurmaId = turmaId;
        EntregaId = entregaId;
    }

    public override bool EstaValido()
    {
        ValidationResult = new AtualizarNotaValidation().Validate(this);
        return ValidationResult.IsValid;
    }

    public class AtualizarNotaValidation : AbstractValidator<AtualizarNotaCommand>
    {
        public AtualizarNotaValidation()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty).WithMessage("O ID da nota é inválido");

            RuleFor(x => x.ValorNota)
                .GreaterThanOrEqualTo(0).WithMessage("A nota deve ser maior ou igual a zero")
                .LessThanOrEqualTo(10).WithMessage("A nota deve ser menor ou igual a 10");

            RuleFor(x => x.AlunoId)
                .NotEqual(Guid.Empty).WithMessage("O ID do aluno é inválido");

            RuleFor(x => x.TurmaId)
                .NotEqual(Guid.Empty).WithMessage("O ID da turma é inválido");

            RuleFor(x => x.EntregaId)
                .NotEqual(Guid.Empty).WithMessage("O ID da entrega é inválido");
        }
    }
}